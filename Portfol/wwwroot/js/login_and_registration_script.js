document.addEventListener('DOMContentLoaded', function () {
    const signInBtn = document.querySelector('.signin-btn');
    const signUpBtn = document.querySelector('.signup-btn');
    const formBox = document.querySelector('.form-box');
    const block = document.querySelector('.block');

    if (signInBtn && signUpBtn) {
        signUpBtn.addEventListener('click', function () {
            formBox.classList.add('active');
            block.classList.add('active');
        });

        signInBtn.addEventListener('click', function () {
            formBox.classList.remove('active');
            block.classList.remove('active');
        });
    }

    function hiddenOpen_CloseClick(selector) {
        let x = document.querySelector(selector);
        if (x.style.display === "none") {
            x.style.display = "grid";
        } else {
            x.style.display = "none";
        }
    }

    document.getElementById("click-to-hide").addEventListener("click", function () {
        hiddenOpen_CloseClick(".container-login-registration");
        formBox.classList.remove('active');
        block.classList.remove('active');
    });

    document.querySelector(".overlay").addEventListener("click", function () {
        hiddenOpen_CloseClick(".container-login-registration");
    });
    document.getElementById('hamburger').addEventListener('click', toggleMenu);
    document.getElementById("side-menu-button-click-to-hide").addEventListener("click", function () {
        hiddenOpen_CloseClick();
        formBox.classList.remove('active');
        block.classList.remove('active');
    });

    const form_btn_signin = document.querySelector('.form_btn_signin');
    const form_btn_signup = document.querySelector('.form_btn_signup');

    if (form_btn_signin) {
        form_btn_signin.addEventListener('click', function () {
            const requestURL = '/Home/Login';

            const errorContainer = document.getElementById('error-messages-singin');

            const form = {
                email: document.getElementById("signin_email"),
                password: document.getElementById("signin_password")
            };

            const body = {
                email: form.email.value,
                password: form.password.value
            };

            sendRequest("POST", requestURL, body)
                .then(data => {
                    cleaningAndClosingForm(form, errorContainer);
                    console.log('Успешный ответ:', data);
                    hiddenOpen_CloseClick(".confirm-email-container");
                    confirmEmail(data);
                })
                .catch(err => {
                    displayErrors(err, errorContainer);
                    console.log(err);
                });
        });
    }
    if (form_btn_signup) {
        form_btn_signup.addEventListener('click', function () {
            const requestURL = '/Home/Register';

            const errorContainer = document.getElementById('error-messages-singup');

            const form = {
                login: document.getElementById("signup_login"),
                email: document.getElementById("signup_email"),
                password: document.getElementById("signup_password"),
                passwordConfirm: document.getElementById("signup_confirm_password"),
            };

            const body = {
                login: form.login.value,
                email: form.email.value,
                password: form.password.value,
                passwordConfirm: form.passwordConfirm.value,
            };
            sendRequest('POST', requestURL, body)
                .then(data => {
                    cleaningAndClosingForm(form, errorContainer);
                    console.log('Успешный ответ:', data);

                    hiddenOpen_CloseClick(".confirm__email__container");
                    confirmEmail(data);


                })
                .catch(err => {
                    displayErrors(err, errorContainer);
                    console.log(err);
                });
        });
    }

    function sendRequest(method, url, body = null) {
        const headers = {
            'Content-Type': 'application/json'
        };
        return fetch(url, {
            method: method,
            body: JSON.stringify(body),
            headers: headers
        })
            .then(response => {
                if (!response.ok) {
                    return response.json().then(errorData => {
                        throw errorData;
                    });
                }
                return response.json();
            });
    }

    const requestURL = '/Home/Login';

    const errorContainer = document.getElementById('error-message-signin');

    const form = {
        email: document.querySelector("#signin_email input"),
        password: document.querySelector("#signin_password input")
    }

    function displayErrors(errors, errorContainer) {
        errorContainer.innerHTML = ''; // Очистить предыдущие ошибки
        errors.forEach(error => {
            const errorMessage = document.createElement('div');
            errorMessage.classList.add('error');
            errorMessage.textContent = error;
            errorContainer.appendChild(errorMessage);
        });
    }

    function cleaningAndClosingForm(form, errorContainer) {
        errorContainer.innerHTML = ''; // Очистка контейнера для ошибок

        for (const key in form) {
            if (form.hasOwnProperty(key)) {
                form[key].value = ''; // Сброс значений полей формы
            }
        }


    }

    function confirmEmail(body) {
        document.querySelector(".button__confirm").addEventListener("click", function () {
            body.codeConfirm = document.getElementById('codeConfirm').value;
            const requestURL = "/Home/ConfirmEmail";
            sendRequest("POST", requestURL, body)
                .then(data => {
                    console.log("Код подтверждения:", data);

                    hiddenOpen_CloseClick(".confirm__email__container");

                    location.reload();
                })
                .catch(err => {
                    const errorContainer = document.querySelector(".warning");
                    displayErrors(err, errorContainer);

                    console.log(err);
                });
        });
    }


    document.querySelector(".button__confirm__close").addEventListener("click", function () {
        hiddenOpen_CloseClick(".confirm__email__container");
    });

    const google = document.querySelectorAll('.google');

    if (google) {
        google.forEach(btn => {
            btn.addEventListener('click', function () {
                window.location.href = `/Home/AuthenticationGoogle?returnUrl=${encodeURIComponent(window.location.href)}`;
            });
        });
    }
});

    