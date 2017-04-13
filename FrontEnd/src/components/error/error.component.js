import template from './error.html';
import controller from './error.controller';

export default {
    template,
    controller,
    bindings: {
        active: '=',
        message: '<'
    }
}