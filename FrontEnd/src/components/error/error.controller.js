controller.$inject = [];

function controller() {
    const $ctrl = this;

    $ctrl.dismiss = dismiss;

    function dismiss() {
        $ctrl.active = false;
    }
}

export default controller;