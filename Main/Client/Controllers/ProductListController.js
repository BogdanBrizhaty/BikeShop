app.controller('ProductListController', ['$scope', '$http', '$location', 'ProductService', function ($scope, $http, $location, ProductService) {

    $scope.goToPage = (page) => {
        if (page == $scope.currentPage)
            return;

        ProductService.getProducts(page).then((e) => {
            $scope.ProductList = e.data;
            $scope.currentPage = e.headers('X-current-page');
            $scope.totalPages = e.headers('X-total-pages');
            $scope.totalItems = e.headers('X-total-items');
        })
    };

    $scope.goToPage(1);
}]);