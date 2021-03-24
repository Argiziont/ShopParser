/* tslint:disable */
/* eslint-disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.10.2.0 (NJsonSchema v10.3.4.0 (Newtonsoft.Json v11.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming

export class ProductClient {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        this.http = http ? http : <any>window;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "https://localhost:5001";
    }

    parseDataInsideProductPage(productUrl: string | null | undefined): Promise<ProductData> {
        let url_ = this.baseUrl + "/Product/ParseProductPage?";
        if (productUrl !== undefined && productUrl !== null)
            url_ += "productUrl=" + encodeURIComponent("" + productUrl) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processParseDataInsideProductPage(_response);
        });
    }

    protected processParseDataInsideProductPage(response: Response): Promise<ProductData> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = ProductData.fromJS(resultData200);
            return result200;
            });
        } else {
            return response.text().then((_responseText) => {
            let resultdefault: any = null;
            let resultDatadefault = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
            });
        }
    }

    parseAllProductUrlsInsideSellerPage(shopName: string | null | undefined): Promise<ProductData> {
        let url_ = this.baseUrl + "/Product/ParseAllProductUrlsInsideSellerPage?";
        if (shopName !== undefined && shopName !== null)
            url_ += "shopName=" + encodeURIComponent("" + shopName) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "POST",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processParseAllProductUrlsInsideSellerPage(_response);
        });
    }

    protected processParseAllProductUrlsInsideSellerPage(response: Response): Promise<ProductData> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = ProductData.fromJS(resultData200);
            return result200;
            });
        } else if (status === 400) {
            return response.text().then((_responseText) => {
            let result400: any = null;
            let resultData400 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result400 = resultData400 !== undefined ? resultData400 : <any>null;
            return throwException("A server side error occurred.", status, _responseText, _headers, result400);
            });
        } else {
            return response.text().then((_responseText) => {
            let resultdefault: any = null;
            let resultDatadefault = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
            });
        }
    }

    parseSingleProductInsideSellerPage(productId: string | null | undefined): Promise<ProductData> {
        let url_ = this.baseUrl + "/Product/ParseSingleProductInsideSellerPage?";
        if (productId !== undefined && productId !== null)
            url_ += "productId=" + encodeURIComponent("" + productId) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "POST",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processParseSingleProductInsideSellerPage(_response);
        });
    }

    protected processParseSingleProductInsideSellerPage(response: Response): Promise<ProductData> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = ProductData.fromJS(resultData200);
            return result200;
            });
        } else if (status === 202) {
            return response.text().then((_responseText) => {
            let result202: any = null;
            let resultData202 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result202 = resultData202 !== undefined ? resultData202 : <any>null;
            return throwException("A server side error occurred.", status, _responseText, _headers, result202);
            });
        } else if (status === 400) {
            return response.text().then((_responseText) => {
            let result400: any = null;
            let resultData400 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result400 = resultData400 !== undefined ? resultData400 : <any>null;
            return throwException("A server side error occurred.", status, _responseText, _headers, result400);
            });
        } else {
            return response.text().then((_responseText) => {
            let resultdefault: any = null;
            let resultDatadefault = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
            });
        }
    }

    getProductsByShopId(id: number | undefined): Promise<ResponseProduct[]> {
        let url_ = this.baseUrl + "/Product/GetPagesByShopId?";
        if (id === null)
            throw new Error("The parameter 'id' cannot be null.");
        else if (id !== undefined)
            url_ += "id=" + encodeURIComponent("" + id) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "POST",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processGetProductsByShopId(_response);
        });
    }

    protected processGetProductsByShopId(response: Response): Promise<ResponseProduct[]> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(ResponseProduct.fromJS(item));
            }
            return result200;
            });
        } else if (status === 500) {
            return response.text().then((_responseText) => {
            let result500: any = null;
            let resultData500 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result500 = Exception.fromJS(resultData500);
            return throwException("A server side error occurred.", status, _responseText, _headers, result500);
            });
        } else {
            return response.text().then((_responseText) => {
            let resultdefault: any = null;
            let resultDatadefault = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
            });
        }
    }
}

export class ShopClient {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        this.http = http ? http : <any>window;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "https://localhost:5001";
    }

    addProductsListFromSeller(sellerName: string | null | undefined): Promise<string[]> {
        let url_ = this.baseUrl + "/Shop/ParseSellerPageProducts?";
        if (sellerName !== undefined && sellerName !== null)
            url_ += "sellerName=" + encodeURIComponent("" + sellerName) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processAddProductsListFromSeller(_response);
        });
    }

    protected processAddProductsListFromSeller(response: Response): Promise<string[]> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(item);
            }
            return result200;
            });
        } else if (status === 400) {
            return response.text().then((_responseText) => {
            let result400: any = null;
            let resultData400 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result400 = Exception.fromJS(resultData400);
            return throwException("A server side error occurred.", status, _responseText, _headers, result400);
            });
        } else {
            return response.text().then((_responseText) => {
            let resultdefault: any = null;
            let resultDatadefault = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
            });
        }
    }

    findDataInsideSellerPage(sellerUrl: string | null | undefined): Promise<ShopJson> {
        let url_ = this.baseUrl + "/Shop/ParseSellerPage?";
        if (sellerUrl !== undefined && sellerUrl !== null)
            url_ += "sellerUrl=" + encodeURIComponent("" + sellerUrl) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processFindDataInsideSellerPage(_response);
        });
    }

    protected processFindDataInsideSellerPage(response: Response): Promise<ShopJson> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = ShopJson.fromJS(resultData200);
            return result200;
            });
        } else if (status === 400) {
            return response.text().then((_responseText) => {
            let result400: any = null;
            let resultData400 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result400 = Exception.fromJS(resultData400);
            return throwException("A server side error occurred.", status, _responseText, _headers, result400);
            });
        } else {
            return response.text().then((_responseText) => {
            let resultdefault: any = null;
            let resultDatadefault = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
            });
        }
    }

    getProductsByShopId(id: number | undefined): Promise<ResponseShop> {
        let url_ = this.baseUrl + "/Shop/GetShopById?";
        if (id === null)
            throw new Error("The parameter 'id' cannot be null.");
        else if (id !== undefined)
            url_ += "id=" + encodeURIComponent("" + id) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "POST",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processGetProductsByShopId(_response);
        });
    }

    protected processGetProductsByShopId(response: Response): Promise<ResponseShop> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = ResponseShop.fromJS(resultData200);
            return result200;
            });
        } else if (status === 500) {
            return response.text().then((_responseText) => {
            let result500: any = null;
            let resultData500 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result500 = Exception.fromJS(resultData500);
            return throwException("A server side error occurred.", status, _responseText, _headers, result500);
            });
        } else {
            return response.text().then((_responseText) => {
            let resultdefault: any = null;
            let resultDatadefault = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
            });
        }
    }
}

export class ProductData implements IProductData {
    id?: number;
    shopId?: number | undefined;
    externalId?: string | undefined;
    title?: string | undefined;
    url?: string | undefined;
    syncDate?: Date;
    expirationDate?: Date;
    productState?: ProductState;
    description?: string | undefined;
    price?: string | undefined;
    jsonData?: string | undefined;
    jsonDataSchema?: string | undefined;
    shop?: ShopData | undefined;

    constructor(data?: IProductData) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.shopId = _data["shopId"];
            this.externalId = _data["externalId"];
            this.title = _data["title"];
            this.url = _data["url"];
            this.syncDate = _data["syncDate"] ? new Date(_data["syncDate"].toString()) : <any>undefined;
            this.expirationDate = _data["expirationDate"] ? new Date(_data["expirationDate"].toString()) : <any>undefined;
            this.productState = _data["productState"];
            this.description = _data["description"];
            this.price = _data["price"];
            this.jsonData = _data["jsonData"];
            this.jsonDataSchema = _data["jsonDataSchema"];
            this.shop = _data["shop"] ? ShopData.fromJS(_data["shop"]) : <any>undefined;
        }
    }

    static fromJS(data: any): ProductData {
        data = typeof data === 'object' ? data : {};
        let result = new ProductData();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["shopId"] = this.shopId;
        data["externalId"] = this.externalId;
        data["title"] = this.title;
        data["url"] = this.url;
        data["syncDate"] = this.syncDate ? this.syncDate.toISOString() : <any>undefined;
        data["expirationDate"] = this.expirationDate ? this.expirationDate.toISOString() : <any>undefined;
        data["productState"] = this.productState;
        data["description"] = this.description;
        data["price"] = this.price;
        data["jsonData"] = this.jsonData;
        data["jsonDataSchema"] = this.jsonDataSchema;
        data["shop"] = this.shop ? this.shop.toJSON() : <any>undefined;
        return data; 
    }
}

export interface IProductData {
    id?: number;
    shopId?: number | undefined;
    externalId?: string | undefined;
    title?: string | undefined;
    url?: string | undefined;
    syncDate?: Date;
    expirationDate?: Date;
    productState?: ProductState;
    description?: string | undefined;
    price?: string | undefined;
    jsonData?: string | undefined;
    jsonDataSchema?: string | undefined;
    shop?: ShopData | undefined;
}

export enum ProductState {
    Idle = 0,
    Success = 1,
    Failed = 2,
}

export class ShopData implements IShopData {
    id?: number;
    sourceId?: number | undefined;
    externalId?: string | undefined;
    name?: string | undefined;
    url?: string | undefined;
    syncDate?: Date;
    jsonData?: string | undefined;
    jsonDataSchema?: string | undefined;
    products?: ProductData[] | undefined;
    source?: ShopSource | undefined;

    constructor(data?: IShopData) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.sourceId = _data["sourceId"];
            this.externalId = _data["externalId"];
            this.name = _data["name"];
            this.url = _data["url"];
            this.syncDate = _data["syncDate"] ? new Date(_data["syncDate"].toString()) : <any>undefined;
            this.jsonData = _data["jsonData"];
            this.jsonDataSchema = _data["jsonDataSchema"];
            if (Array.isArray(_data["products"])) {
                this.products = [] as any;
                for (let item of _data["products"])
                    this.products!.push(ProductData.fromJS(item));
            }
            this.source = _data["source"] ? ShopSource.fromJS(_data["source"]) : <any>undefined;
        }
    }

    static fromJS(data: any): ShopData {
        data = typeof data === 'object' ? data : {};
        let result = new ShopData();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["sourceId"] = this.sourceId;
        data["externalId"] = this.externalId;
        data["name"] = this.name;
        data["url"] = this.url;
        data["syncDate"] = this.syncDate ? this.syncDate.toISOString() : <any>undefined;
        data["jsonData"] = this.jsonData;
        data["jsonDataSchema"] = this.jsonDataSchema;
        if (Array.isArray(this.products)) {
            data["products"] = [];
            for (let item of this.products)
                data["products"].push(item.toJSON());
        }
        data["source"] = this.source ? this.source.toJSON() : <any>undefined;
        return data; 
    }
}

export interface IShopData {
    id?: number;
    sourceId?: number | undefined;
    externalId?: string | undefined;
    name?: string | undefined;
    url?: string | undefined;
    syncDate?: Date;
    jsonData?: string | undefined;
    jsonDataSchema?: string | undefined;
    products?: ProductData[] | undefined;
    source?: ShopSource | undefined;
}

export class ShopSource implements IShopSource {
    id?: number;
    name?: string | undefined;
    shops?: ShopData[] | undefined;

    constructor(data?: IShopSource) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
            if (Array.isArray(_data["shops"])) {
                this.shops = [] as any;
                for (let item of _data["shops"])
                    this.shops!.push(ShopData.fromJS(item));
            }
        }
    }

    static fromJS(data: any): ShopSource {
        data = typeof data === 'object' ? data : {};
        let result = new ShopSource();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        if (Array.isArray(this.shops)) {
            data["shops"] = [];
            for (let item of this.shops)
                data["shops"].push(item.toJSON());
        }
        return data; 
    }
}

export interface IShopSource {
    id?: number;
    name?: string | undefined;
    shops?: ShopData[] | undefined;
}

export class ProblemDetails implements IProblemDetails {
    type?: string | undefined;
    title?: string | undefined;
    status?: number | undefined;
    detail?: string | undefined;
    instance?: string | undefined;

    constructor(data?: IProblemDetails) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.type = _data["type"];
            this.title = _data["title"];
            this.status = _data["status"];
            this.detail = _data["detail"];
            this.instance = _data["instance"];
        }
    }

    static fromJS(data: any): ProblemDetails {
        data = typeof data === 'object' ? data : {};
        let result = new ProblemDetails();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["type"] = this.type;
        data["title"] = this.title;
        data["status"] = this.status;
        data["detail"] = this.detail;
        data["instance"] = this.instance;
        return data; 
    }
}

export interface IProblemDetails {
    type?: string | undefined;
    title?: string | undefined;
    status?: number | undefined;
    detail?: string | undefined;
    instance?: string | undefined;
}

export class ResponseProduct implements IResponseProduct {
    id?: number;
    externalId?: string | undefined;
    title?: string | undefined;
    url?: string | undefined;
    syncDate?: Date;
    description?: string | undefined;
    price?: string | undefined;

    constructor(data?: IResponseProduct) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.externalId = _data["externalId"];
            this.title = _data["title"];
            this.url = _data["url"];
            this.syncDate = _data["syncDate"] ? new Date(_data["syncDate"].toString()) : <any>undefined;
            this.description = _data["description"];
            this.price = _data["price"];
        }
    }

    static fromJS(data: any): ResponseProduct {
        data = typeof data === 'object' ? data : {};
        let result = new ResponseProduct();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["externalId"] = this.externalId;
        data["title"] = this.title;
        data["url"] = this.url;
        data["syncDate"] = this.syncDate ? this.syncDate.toISOString() : <any>undefined;
        data["description"] = this.description;
        data["price"] = this.price;
        return data; 
    }
}

export interface IResponseProduct {
    id?: number;
    externalId?: string | undefined;
    title?: string | undefined;
    url?: string | undefined;
    syncDate?: Date;
    description?: string | undefined;
    price?: string | undefined;
}

export class Exception implements IException {
    stackTrace?: string | undefined;
    message?: string;
    innerException?: Exception | undefined;
    source?: string | undefined;

    constructor(data?: IException) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.stackTrace = _data["StackTrace"];
            this.message = _data["Message"];
            this.innerException = _data["InnerException"] ? Exception.fromJS(_data["InnerException"]) : <any>undefined;
            this.source = _data["Source"];
        }
    }

    static fromJS(data: any): Exception {
        data = typeof data === 'object' ? data : {};
        let result = new Exception();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["StackTrace"] = this.stackTrace;
        data["Message"] = this.message;
        data["InnerException"] = this.innerException ? this.innerException.toJSON() : <any>undefined;
        data["Source"] = this.source;
        return data; 
    }
}

export interface IException {
    stackTrace?: string | undefined;
    message?: string;
    innerException?: Exception | undefined;
    source?: string | undefined;
}

export class ShopJson implements IShopJson {
    externalId?: string | undefined;
    name?: string | undefined;
    url?: string | undefined;
    syncDate?: Date;

    constructor(data?: IShopJson) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.externalId = _data["externalId"];
            this.name = _data["name"];
            this.url = _data["url"];
            this.syncDate = _data["syncDate"] ? new Date(_data["syncDate"].toString()) : <any>undefined;
        }
    }

    static fromJS(data: any): ShopJson {
        data = typeof data === 'object' ? data : {};
        let result = new ShopJson();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["externalId"] = this.externalId;
        data["name"] = this.name;
        data["url"] = this.url;
        data["syncDate"] = this.syncDate ? this.syncDate.toISOString() : <any>undefined;
        return data; 
    }
}

export interface IShopJson {
    externalId?: string | undefined;
    name?: string | undefined;
    url?: string | undefined;
    syncDate?: Date;
}

export class ResponseShop implements IResponseShop {
    id?: number;
    externalId?: string | undefined;
    name?: string | undefined;
    url?: string | undefined;
    syncDate?: Date;

    constructor(data?: IResponseShop) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.externalId = _data["externalId"];
            this.name = _data["name"];
            this.url = _data["url"];
            this.syncDate = _data["syncDate"] ? new Date(_data["syncDate"].toString()) : <any>undefined;
        }
    }

    static fromJS(data: any): ResponseShop {
        data = typeof data === 'object' ? data : {};
        let result = new ResponseShop();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["externalId"] = this.externalId;
        data["name"] = this.name;
        data["url"] = this.url;
        data["syncDate"] = this.syncDate ? this.syncDate.toISOString() : <any>undefined;
        return data; 
    }
}

export interface IResponseShop {
    id?: number;
    externalId?: string | undefined;
    name?: string | undefined;
    url?: string | undefined;
    syncDate?: Date;
}

export class ApiException extends Error {
    message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isApiException = true;

    static isApiException(obj: any): obj is ApiException {
        return obj.isApiException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): any {
    throw new ApiException(message, status, response, headers, result);
}