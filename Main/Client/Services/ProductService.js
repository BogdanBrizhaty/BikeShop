app.service('ProductService', function ($http) {
    var getProduct = (id) => {
        return $http.get('/api/products/' + id);
    }
    var getProducts = (page) => {
        page = page >= 1 ? page : 1;
        return $http.get('/api/products/page/' + page);
    }
    var findProducts = (q, page) => {
        page = page >= 1 ? page : 1;
        return $http.get('/api/products/search?q=' + q + '&page=' + page);
    }
    var getMostBuyableProducts = () => {
        return $http.get('/api/products/top');
    }

    return {
        getProduct : getProduct,
        getProducts : getProducts,
        findProducts: findProducts,
        getMostBuyableProducts: getMostBuyableProducts
    };
});