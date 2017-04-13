controller.$inject = ['$location', 'AuthService'];

function controller($location, AuthService) {
    const $ctrl = this;

    $ctrl.username = '';
    $ctrl.password = '';
    $ctrl.isLoading = false;
    $ctrl.isError = false;
    $ctrl.register = register;

    function register() {
        $ctrl.isLoading = true;
        $ctrl.isError = false;
        AuthService.register({
            username: $ctrl.username,
            email: $ctrl.email,
            password: $ctrl.password,
            confirmPassword: $ctrl.confirmPassword
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