app.controller('ProductListController', ['$scope', '$http', '$location', 'ProductService', function ($scope, $http, $location, ProductService) {

    $scope.goToPage = (page) => {
        if (page == $scope.currentPage)
            return;

        ProductService.getProducts(page).then((e) => {
            //console.log('opening: ' + page);
            $scope.ProductList = e.data;
            //console.log($scope.ProductList);
            $scope.currentPage = e.headers('X-current-page');
            $scope.totalPages = e.headers('X-total-pages');
            $scope.totalItems = e.headers('X-total-items');
            //console.log('Items: ' + $scope.totalItems);
            //console.log('Pages: ' + $scope.totalPages);
            //console.log('Cur page: ' + $scope.currentPage);
        })
    };

    $scope.goToPage(1);
}]);