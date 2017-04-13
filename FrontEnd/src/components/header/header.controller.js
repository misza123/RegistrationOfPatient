controller.$inject = ['$route', 'AuthService'];

function controller($route, AuthService) {
    const $ctrl = this;

    $ctrl.isLoading = false;
    $ctrl.isError = false;
    $ctrl.isRouteActive = isRouteActive;
    $ctrl.isLoggedIn = AuthService.isLoggedIn;
    $ctrl.getUser = AuthService.getUser;
    $ctrl.logout = logout;

    function isRouteActive(route) {
        return route === '/' ? isRootLocation(route) : isDescendantLocation(route);
    }

    function isRootLocation(route) {
        return $route.current &&
            $route.current.$$route &&
            $route.current.$$route.originalPath === route;
    }

    function isDescendantLocation(route) {
        return $route.current &&
            $route.current.$$route &&
            $route.current.$$route.originalPath &&
            $route.current.$$route.originalPath.startsWith(route);
    }

    function logout() {
        $ctrl.isLoading = true;
        $ctrl.isError = false;
        AuthService.logout()
            .then(() => {
                $ctrl.isLoading = false;
                $route.reload();
            }, (error) => {
                $ctrl.isLoading = false;
                $ctrl.isError = true;
                $ctrl.error = error.message;
            });
    }
}

export default controller;