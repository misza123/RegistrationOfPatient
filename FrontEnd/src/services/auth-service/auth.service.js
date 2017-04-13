service.$inject = ['$http', '$q'];

function service($http, $q) {
    let user = null;

    return {
        isLoggedIn,
        getUser,
        login,
        register,
        logout
    }

    function login({ username, password }) {
        return $http.post('/api/login', {
            username,
            password
        }).then((response) => {
            user = response.data;
        });
    }

    function register({ username, email, password, confirmPassword }) {
        if (password !== confirmPassword)
            return $q.reject({
                message: 'Podane hasła nie pasują do siebie.'
            });

        return $http.post('/api/register', {
            username,
            email,
            password
        }).then(() => {
            return login({
                username,
                password
            })
        });

    }

    function logout() {
        return $http.post('/api/logout')
            .then(() => {
                user = null;
            });;
    }

    function isLoggedIn() {
        return !!user;
    }

    function getUser() {
        return user;
    }
}

export default service;