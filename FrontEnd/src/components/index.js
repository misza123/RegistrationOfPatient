import angular from 'angular';

import header from './header/header.component';
import footer from './footer/footer.component';
import loginRegister from './loginRegister/loginRegister.component';

const app = angular.module('patient.components', []);

app.component('appHeader', header);
app.component('appFooter', footer);
app.component('loginRegister', loginRegister);

export default app.name;