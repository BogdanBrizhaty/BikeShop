app.controller('SearchController', ['$scope', '$routeParams', 'ProductService', function ($scope, $routeParams, ProductService) {
    //console.log($routeParams.q);
    $scope.q = $routeParams.q;
    console.log($scope.q);

    $scope.goToPage = (page) => {
        if (page == $scope.currentPage)
            return;

        ProductService.findProducts($scope.q, page).then((e) => {
            $scope.ProductList = e.data;
            $scope.currentPage = e.headers('X-current-page');
            $scope.totalPages = e.headers('X-total-pages');
            $scope.totalItems = e.headers('X-total-items');
        })
    };

    $scope.goToPage(1);
}]);