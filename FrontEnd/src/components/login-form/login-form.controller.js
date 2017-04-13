controller.$inject = ['$location', 'AuthService'];

function controller($location, AuthService) {
    const $ctrl = this;

    $ctrl.username = '';
    $ctrl.password = '';
    $ctrl.isLoading = false;
    $ctrl.isError = false;
    $ctrl.login = login;

    function login() {
        $ctrl.isLoading = true;
        $ctrl.isError = false;
        AuthService.login({
            username: $ctrl.username,
            password: $ctrl.password
        }).then(() => {
            $ctrl.isLoading = false;
            redirectToHome();
        }, (error) => {
            $ctrl.isLoading = false;
            $ctrl.isError = true;
            $ctrl.error = error.message || error.statusText;
        });
    }

    function redirectToHome() {
        $location.path('/');
    }
}

export default controller;