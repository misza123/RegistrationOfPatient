authCheck.$inject = ['$rootScope', '$location', 'AuthService'];

function authCheck($rootScope, $location, AuthService) {
    $rootScope.$on('$routeChangeStart', (event) => {
        if (AuthService.isLoggedIn() || $location.path() === '/login') return;

        event.preventDefault();
        $location.path('/login');
    });
}

export default authCheck;