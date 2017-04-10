import angular from 'angular';
import ngRoute from 'angular-route';

import components from './components';
import services from './services';
import values from './values';

import routes from './routes';

import 'bulma';
import 'font-awesome/scss/font-awesome.scss';

const app = angular.module('patient.main', [ngRoute, components, services, values]);

app.config(routes);

export default app.name;