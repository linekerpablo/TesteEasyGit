app.controller("MarcaController", function ($scope, $window, MarcaServico, toastr) {
    $scope.gridOptions = {
        data: [],
        urlSync: true,
        pagination: {
            itemsPerPage: '10'
        }
    };

    $scope.carregarMarca = function () {
        var idMarca = $window.location.pathname.split('/')[3];

        MarcaServico.carregarMarca(idMarca).then(function (data) {
            if (data.marca !== undefined && data.erros === undefined) {
                $scope.marca = data.marca;
            }
            else {
                toastr.error(data.erros, 'Erro');
            }
        });
    };

    $scope.carregarMarcas = function () {
        MarcaServico.carregarMarcas().then(function (data) {
            if (data.lstMarcas !== undefined && data.erros === undefined) {
                $scope.lstMarcas = data.lstMarcas;
                $scope.gridOptions.data = data.lstMarcas;
            }
            else {
                toastr.error(data.erros, 'Erro');
            }
        });
    };

    $scope.cadastrar = function () {
        $scope.subimeter = true;

        if ($scope.frmCadastrar.$valid) {
            MarcaServico.cadastrar($scope.Descricao).then(function (data) {
                if (data.erros === undefined) {
                    toastr.success(data.mensagem, 'Sucesso');

                    $scope.subimeter = false;
                }
                else {
                    toastr.error(data.erros, 'Erro');

                    $scope.subimeter = false;
                }

                if (data.urlRedirecionar !== undefined) {
                    $window.location.href = data.urlRedirecionar;
                }
            });
        }
    };

    $scope.editar = function () {
        $scope.subimeter = true;

        if ($scope.frmEditar.$valid) {
            MarcaServico.editar($scope.marca).then(function (data) {
                if (data.erros === undefined) {
                    toastr.success(data.mensagem, 'Sucesso');

                    $scope.subimeter = false;
                }
                else {
                    toastr.error(data.erros, 'Erro');

                    $scope.subimeter = false;
                }

                if (data.urlRedirecionar !== undefined) {
                    $window.location.href = data.urlRedirecionar;
                }
            });
        }
    };

    $scope.deletar = function (marca) {
        MarcaServico.deletar(marca).then(function (data) {
            if (data.erros === undefined) {
                toastr.success(data.mensagem, 'Sucesso');
            }
            else {
                toastr.error(data.erros, 'Erro');
            }

            if (data.urlRedirecionar !== undefined) {
                $window.location.href = data.urlRedirecionar;
            }
        });
    };
});