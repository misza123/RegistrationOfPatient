import angular from 'angular';

import header from './header/header.component';

const app = angular.module('patient.components', []);

app.component('header', header);

export default app.name;