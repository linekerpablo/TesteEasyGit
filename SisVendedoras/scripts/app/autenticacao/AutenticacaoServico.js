app.service('AutenticacaoServico', ["$http", "$q", function ($http, $q) {
    return {
        login: function (email, senha) {
            return $http({
                url: "/Login/Login",
                method: "POST",
                data: {
                    email: email,
                    senha: senha
                }
            })
            .then(
                function (response) {
                    return response.data;
                },
                function (errResponse) {
                    return $q.reject(errResponse);
                }
            );
        }
    }
}]);