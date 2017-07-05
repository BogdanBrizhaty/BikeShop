app.directive('productPaging', ['$location', function ($location) {
    return {
        restrict: 'E',
        scope: true,
        template: '<ul class="pagination"><li><a ng-click="showPreviousPages()">&laquo;</a></li>' +
            '<li ng-repeat="pageNum in pages"' +
            'ng-class="pageNum == currentPage ? \'active\' : \'\'">' +
            '<a ng-click="goToPage(pageNum)">{{pageNum}}</a></li>' +
            '<li><a ng-click="showNextPages()" >&raquo;</a></li></ul>' +
            '<div ng-bind="totalItems"></div>',
        link: (scope, element) => {
            var pagesAmountToDisplay = 10;

            var getPages = (curPage) => {
                var tmp = [];
                var startAt = Math.ceil(curPage / pagesAmountToDisplay) * pagesAmountToDisplay - pagesAmountToDisplay + 1;
                if (startAt + pagesAmountToDisplay - 1 > scope.totalPages) {
                    if (scope.totalPages - scope.pages[pagesAmountToDisplay - 1] == 0)
                        return scope.pages;
                    for (var i = startAt - pagesAmountToDisplay + 1; i < startAt + pagesAmountToDisplay && i <= scope.totalPages; i += scope.totalPages - scope.pages[pagesAmountToDisplay - 1]) {
                        console.log(scope.totalPages - scope.pages[pagesAmountToDisplay - 1]);
                        tmp.push(i);
                    }
                    return tmp;
                }
                for (var i = startAt; i < startAt + pagesAmountToDisplay && i <= scope.totalPages; i++)
                    tmp.push(i);
                return tmp;
            }
            scope.showNextPages = () => {
                if ((scope.pages[pagesAmountToDisplay - 1] + 1 <= scope.totalPages))
                    scope.pages = getPages(scope.pages[scope.pages.length - 1] + 1);
            }
            scope.showPreviousPages = () => {
                if ((scope.pages[0] - 1) > 0)
                    scope.pages = getPages(scope.pages[0] - 1);
            }
            scope.$watch('totalItems', (value, old) => {
                scope.pages = getPages(scope.currentPage);
            });
        }
    };
}]);