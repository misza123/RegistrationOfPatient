import angular from 'angular';

import header from './header/header.component';
import footer from './footer/footer.component';
import error from './error/error.component';
import loginRegister from './login-register/login-register.component';
import loginForm from './login-form/login-form.component';
import registerForm from './register-form/register-form.component';

const app = angular.module('patient.components', []);

app.component('appHeader', header);
app.component('appFooter', footer);
app.component('error', error);
app.component('loginRegister', loginRegister);
app.component('loginForm', loginForm);
app.component('registerForm', registerForm);

export default app.name;