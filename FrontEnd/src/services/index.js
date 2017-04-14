import angular from 'angular';

import AuthService from './auth-service/auth.service';

const app = angular.module('patient.services', []);

app.service('AuthService', AuthService);

export default app.name;