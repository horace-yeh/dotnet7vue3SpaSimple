import axios from "axios";

axios.interceptors.request.use(function (config) {
    // Do something before request is sent
    return config;
  }, function (error) {
    // Do something with request error
    console.error(error);
    return Promise.reject(error);
  });

// Add a response interceptor
axios.interceptors.response.use(function (response) {
    // Do something with response data
    return response;
  }, function (error) {
    // Do something with response error
    console.error(error);
    return Promise.reject(error);
  });

const testRequest = axios.create({
    baseURL: '/api/Values'
});

//test api crud

export const apiGetProductList = () => testRequest.get('/GetProductList');
export const apiAddProduct = data => testRequest.post('/AddProduct', data);
export const apiEditProduct = data => testRequest.post('/EditProduct', data);
export const apiDeleteProduct = data => testRequest.post('/DeleteProduct', data);