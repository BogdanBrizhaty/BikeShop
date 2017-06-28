app.controller('ProductListController', ['$scope', '$http', '$location', 'ProductService', function ($scope, $http, $location, ProductService) {
    var pagesAmountToDisplay = 10;
    //var perPage = 10;
    
    var getPages = (curPage) => {
        var tmp = [];
        var startAt = Math.ceil(curPage / pagesAmountToDisplay) * pagesAmountToDisplay - pagesAmountToDisplay + 1;
        console.log('Start at: ' + startAt);
        if (startAt + pagesAmountToDisplay - 1 > $scope.totalPages) {
            //console.log(startAt - pagesAmountToDisplay + 1);
            if ($scope.totalPages - $scope.pages[pagesAmountToDisplay - 1] == 0)
                return $scope.pages;
            for (var i = startAt - pagesAmountToDisplay + 1; i < startAt + pagesAmountToDisplay && i <= $scope.totalPages; i += $scope.totalPages - $scope.pages[pagesAmountToDisplay - 1]) {
                console.log($scope.totalPages - $scope.pages[pagesAmountToDisplay - 1]);
                tmp.push(i);
                //console.log(i);
            }
            console.log('tmp :' + tmp);
            return tmp;
        }
        for (var i = startAt; i < startAt + pagesAmountToDisplay && i <= $scope.totalPages; i++)
            tmp.push(i);
        return tmp;
    }

    $scope.goToPage = (page) => {
        if (page == $scope.currentPage)
            return;

        ProductService.getProducts(page).then((e) => {
            console.log('opening: ' + page);
            $scope.ProductList = e.data;
            console.log($scope.ProductList);
            $scope.currentPage = e.headers('X-current-page');
            $scope.totalPages = e.headers('X-total-pages');
            $scope.totalItems = e.headers('X-total-items');
            console.log('Items: ' + $scope.totalItems);
            console.log('Pages: ' + $scope.totalPages);
            console.log('Cur page: ' + $scope.currentPage);
            $scope.pages = getPages($scope.currentPage);
            //console.log($scope.pages);
        })
    };

    $scope.goToPage(1);
    $scope.showNextPages = () => {
        if (($scope.pages[pagesAmountToDisplay - 1] + 1 <= $scope.totalPages))
            $scope.pages = getPages($scope.pages[$scope.pages.length - 1] + 1);
    }
    $scope.showPreviousPages = () => {
        if (($scope.pages[0] - 1) > 0)
            $scope.pages = getPages($scope.pages[0] - 1);
    }
    //$scope.maxSize = 10;
}]);