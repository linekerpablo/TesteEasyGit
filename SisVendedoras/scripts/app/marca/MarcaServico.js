app.service('MarcaServico', ["$http", "$q", function ($http, $q) {
    return {
        carregarMarca: function (idMarca) {
            return $http.get("/Marca/CarregarMarca", {
                params: {
                    idMarca: idMarca
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
        },

        carregarMarcas: function () {
            return $http({
                url: "/Marca/CarregarMarcas",
                method: "GET"
            })
            .then(
                function (response) {
                    return response.data;
                },
                function (errResponse) {
                    return $q.reject(errResponse);
                }
            );
        },

        cadastrar: function (descricao) {
            return $http({
                url: "/Marca/Salvar",
                method: "POST",
                data: {
                    Descricao: descricao
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
        },

        editar: function (marca) {
            return $http({
                url: "/Marca/Editar",
                method: "POST",
                data: {
                    objMarca: marca
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
        },

        deletar: function (marca) {
            return $http({
                url: "/Marca/Deletar",
                method: "POST",
                data: {
                    objMarca: marca
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