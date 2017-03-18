import angular from 'angular';

import exampleService from './example/example.service';

const app = angular.module('patient.services', []);

app.service('exampleService', exampleService);

export default app.name;
