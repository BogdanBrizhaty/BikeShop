app.controller('ProductController', ['$scope', '$http', '$routeParams', 'ProductService', ($scope, $http, $routeParams, ProductService) => {
    console.log($routeParams);
    ProductService.getProduct($routeParams.id).then((e) => {
        $scope.ProductInfo = e.data;
    });
}]);