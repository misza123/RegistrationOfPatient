import angular from 'angular';

const app = angular.module('patient.values', []);

app.value('five', 5);

export default app.name;