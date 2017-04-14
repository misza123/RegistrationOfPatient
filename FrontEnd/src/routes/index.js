import homeConfig from './home/home.route';
import loginConfig from './login/login.route';
import redirectConfig from './redirect/redirect.route';

routeConfig.$inject = ['$routeProvider'];

function routeConfig($routeProvider) {
    $routeProvider
        .when('/', homeConfig)
        .when('/login', loginConfig)
        .otherwise(redirectConfig);
}

export default routeConfig;