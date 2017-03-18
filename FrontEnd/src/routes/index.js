import homeConfig from './home/home.route';
import redirectConfig from './redirect/redirect.route';

routeConfig.$inject = ['$routeProvider'];

function routeConfig($routeProvider) {
    $routeProvider
        .when('/', homeConfig)
        .otherwise(redirectConfig);
}

export default routeConfig;
