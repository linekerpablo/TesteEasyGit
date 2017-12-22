app.controller("AutenticacaoController", function ($window, $scope, AutenticacaoServico, toastr) {
    $scope.login = function () {
        $scope.submitted = true;

        if ($scope.frmLogin.$valid) {
            AutenticacaoServico.login($scope.email, $scope.senha).then(function (data) {
                if (data.erros === undefined) {
                    $window.location.href = "/Home/Index";
                }
                else {
                    toastr.error(data.erros, 'Erro');
                }
            });
        }
    };
});