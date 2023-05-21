using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;

public class FireBaseAuth : MonoBehaviour
{
    //Firebase
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;

    //Login
    public InputField log_field;
    public InputField pass_field;

    //Register
    public InputField reg_log_field;
    public InputField reg_pass_field;
    public InputField confirm_field;

    private void Awake()
    {
        //Проверяет наличие всех необходимых зависимостей для Firebase
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

            string err_msg = "Could not log in: ";

            switch (authError)
            {
                case AuthError.InvalidEmail:
                    err_msg += "Incorrect login";
                    break;
                case AuthError.WrongPassword:
                    err_msg += "Incorrect password";
                    break;
                case AuthError.MissingEmail:
                    err_msg += "Missing email";
                    break;
                case AuthError.MissingPassword:
                    err_msg += "Missing password";
                    break;
            }
            Debug.Log(err_msg);
        }
        else
        {
            Debug.Log("You have logged in!");
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
            Debug.LogError("Login field is empty!");
        }
        else if (reg_pass_field.text != confirm_field.text)
        {
            Debug.LogError("Passwords don't match!");
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

                string err_msg = "Could not register: ";

                switch (authError)
                {
                    case AuthError.InvalidEmail:
                        err_msg += "Incorrect login";
                        break;
                    case AuthError.MissingEmail:
                        err_msg += "Missing email";
                        break;
                    case AuthError.MissingPassword:
                        err_msg += "Missing password";
                        break;
                }
                Debug.Log(err_msg);
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
                    Debug.Log(err_msg);

                }
                else
                {
                    Debug.Log("Welcome to Pomodoro Tracker!");
                }
            }
        }
    }

    public void Register()
    {
        StartCoroutine(RegisterAsync(reg_log_field.text, reg_pass_field.text, confirm_field.text));
    }
}

