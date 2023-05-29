using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using TMPro;
using Firebase.Database;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;

public class FireBaseAuth : MonoBehaviour
{
    //Firebase
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    public DatabaseReference DBRef;

    //Login
    public InputField log_field;
    public InputField pass_field;

    //Register
    public InputField reg_log_field;
    public InputField reg_pass_field;
    public InputField confirm_field;

    //Warning
    public TMP_Text Warning;

    private void Awake()
    {
        //ѕровер€ет наличие всех необходимых зависимостей дл€ Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
           var dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all firebase dependencies: " + dependencyStatus);
            }
        });
    }

    void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
        DBRef = FirebaseDatabase.DefaultInstance.RootReference; // ѕолучает расположение базы данных 
    }

    void AuthStateChanged(object sender, System.EventArgs e)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;

            if (!signedIn && user != null)
            {
                Debug.Log("Signed Out " + user.UserId);
            }

            user = auth.CurrentUser;

            if (signedIn)
            {
                Debug.Log("Signed In " + user.UserId);
            }
        }
    }

    private IEnumerator LoginAsync(string login, string password)
    {
        var loginTask = auth.SignInWithEmailAndPasswordAsync(login, password);

        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            Debug.LogError(loginTask.Exception);

            FirebaseException fbExc = loginTask.Exception.GetBaseException() as FirebaseException;
            AuthError authError = (AuthError)fbExc.ErrorCode;

            string err_msg = "Login Failed!";

            switch (authError)
            {
                case AuthError.InvalidEmail:
                    err_msg = "Incorrect login";
                    break;
                case AuthError.WrongPassword:
                    err_msg = "Incorrect password";
                    break;
                case AuthError.MissingEmail:
                    err_msg = "Missing email";
                    break;
                case AuthError.MissingPassword:
                    err_msg = "Missing password";
                    break;
                case AuthError.UserNotFound:
                    err_msg = "User not found";
                    break;
            }
            Warning.SetText(err_msg);
        }
        else
        {
            StartCoroutine(LoadData()); // «агрузка данных пользовател€ в случае удачного логина
            SceneManager.LoadScene(0); //«агрузка сцены в случае удачного логина
        }
    }

    public void Login()
    {
        StartCoroutine(LoginAsync(log_field.text, pass_field.text));
    }

    private IEnumerator RegisterAsync(string login, string password, string confirm)
    {
        if (login == "")
        {
            Warning.SetText("Login field is empty!");
        }
        else if (reg_pass_field.text != confirm_field.text)
        {
            Warning.SetText("Passwords don't match!");
        }
        else
        {
            var regTask = auth.CreateUserWithEmailAndPasswordAsync(login, password);

            yield return new WaitUntil(() => regTask.IsCompleted);

            if (regTask.Exception != null)
            {
                Debug.LogError(regTask.Exception);

                FirebaseException fbExc = regTask.Exception.GetBaseException() as FirebaseException;
                AuthError authError = (AuthError)fbExc.ErrorCode;

                string err_msg = "Could not register!";

                switch (authError)
                {
                    case AuthError.InvalidEmail:
                        err_msg = "Incorrect login";
                        break;
                    case AuthError.MissingEmail:
                        err_msg = "Missing email";
                        break;
                    case AuthError.MissingPassword:
                        err_msg = "Missing password";
                        break;
                }
                Warning.SetText(err_msg);
            }
            else
            {
                UserProfile profile = new UserProfile();

                var updateProfileTask = user.UpdateUserProfileAsync(profile);

                yield return new WaitUntil(() => updateProfileTask.IsCompleted);
                if (updateProfileTask.Exception != null)
                {
                    user.DeleteAsync();

                    Debug.LogError(updateProfileTask.Exception);

                    FirebaseException fbExc = updateProfileTask.Exception.GetBaseException() as FirebaseException;
                    AuthError authError = (AuthError)fbExc.ErrorCode;

                    string err_msg = "Could not update profile: ";

                    switch (authError)
                    {
                        case AuthError.InvalidEmail:
                            err_msg = "Incorrect login";
                            break;
                        case AuthError.WrongPassword:
                            err_msg = "Incorrect password";
                            break;
                        case AuthError.MissingEmail:
                            err_msg = "Missing email";
                            break;
                        case AuthError.MissingPassword:
                            err_msg = "Missing password";
                            break;
                    }
                    Warning.SetText(err_msg); 

                }
                else
                {
                    StartCoroutine(UpdateMoney(0)); //”станавливаем значение денег 0, т.к. новый пользователь
                    SceneManager.LoadScene(0);
                }
            }
        }
    }

    public void Register()
    {
        StartCoroutine(RegisterAsync(reg_log_field.text, reg_pass_field.text, confirm_field.text));
    }

    public IEnumerator UpdateMoney(int money) //‘ункци€ обновлени€ денег
    {
        var DBTask = DBRef.Child("users").Child(user.UserId).Child("money").SetValueAsync(money); //≈сли правильно пон€л, обращаетс€ к полю денег пользовател€ через его ID и устанавливает значение через параметр money

        yield return new WaitUntil(() => DBTask.IsCompleted);
        if (DBTask.Exception != null)
        {
            Debug.LogWarning("Fail");
        }
        else
        {
            Debug.Log("Success");
        }
    }

    public IEnumerator LoadData() //‘ункци€ получени€ значени€ денег
    {
        var DBTask = DBRef.Child("users").Child(user.UserId).Child("money").GetValueAsync();//јналогично, если € правильно пон€л, обращаетс€ к полю денег пользовател€ через его ID и получает значение денег

        yield return new WaitUntil(() => DBTask.IsCompleted);
        if (DBTask.Exception != null)
        {
            Debug.LogWarning("Fail");
        }
        else
        {
            Debug.Log("Success");
        }
    }
}

