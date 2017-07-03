app.controller('HomeController', ['$scope', '$http', '$routeParams', 'ProductService', ($scope, $http, $routeParams, ProductService) => {
    $scope.Message = 'Sounds like an oversimplification!';

    ProductService.getMostBuyableProducts().then((e) => {
        $scope.Products = e.data;
    });
}]);