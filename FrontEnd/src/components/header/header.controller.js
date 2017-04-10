controller.$inject = ['$route'];

function controller($route) {
    const $ctrl = this;

    $ctrl.isRouteActive = isRouteActive;

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
}

export default controller;