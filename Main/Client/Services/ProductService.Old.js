app.service('ProductServiceOld', function ($http) {
    var getProduct = (id) => {
        $http.get('/api/products/' + id).then((e) => {
            console.log(e.data);
            //console.log(e.headers('Content-Type'));
            return e.data;
        });
    }
    var getProducts = (page) => {
        page = page >= 1 ? page : 1;
        $http.get('/api/products/page/' + page).then((e) => {
            console.log(e.data);
            return {
                items: e.headers('X-total-items'),
                pages: e.headers('X-total-pages'),
                curPage: e.headers('X-current-page'),
                products: e.data
            };
        });
    }
    var findProducts = (q, page) => {
        page = page >= 1 ? page : 1;
        $http.get('/api/products/search?q=' + q + '&page=' + page).then((e) => {
            console.log(e.data);
            return {
                items: e.headers('X-total-items'),
                pages: e.headers('X-total-pages'),
                curPage: e.headers('X-current-page'),
                products: e.data
            };
        });
    }
    var getTopBuyableProducts = () => {
        $http.get('/api/products/top').then((e) => {
            return e.data;
        });
    }

    return {
        getProduct : getProduct,
        getProducts : getProducts,
        findProducts: findProducts,
        getTopBuyableProducts: getTopBuyableProducts
    };
});