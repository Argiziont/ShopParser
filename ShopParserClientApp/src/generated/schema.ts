import {FieldsSelection,Observable} from '@genql/runtime'

export type Scalars = {
    String: string,
    Boolean: boolean,
    Int: number,
    DateTime: any,
}

export interface QueryService {
    categories?: (ResponseCategory | undefined)[]
    companies?: (ResponseCompany | undefined)[]
    products?: ResponseProductCollectionSegment
    product?: ProductJson
    __typename: 'QueryService'
}

export interface ResponseProductCollectionSegment {
    items?: (ResponseProduct | undefined)[]
    /** Information to aid in pagination. */
    pageInfo: CollectionSegmentInfo
    totalCount: Scalars['Int']
    __typename: 'ResponseProductCollectionSegment'
}

export interface ResponseProduct {
    id: Scalars['Int']
    externalId?: Scalars['String']
    title?: Scalars['String']
    url?: Scalars['String']
    syncDate: Scalars['DateTime']
    description?: Scalars['String']
    price?: Scalars['String']
    __typename: 'ResponseProduct'
}


/** Information about the offset pagination. */
export interface CollectionSegmentInfo {
    /** Indicates whether more items exist following the set defined by the clients arguments. */
    hasNextPage: Scalars['Boolean']
    /** Indicates whether more items exist prior the set defined by the clients arguments. */
    hasPreviousPage: Scalars['Boolean']
    __typename: 'CollectionSegmentInfo'
}

export interface SubscriptionObjectType {
    subscribeProductGetDate?: ProductJson
    __typename: 'SubscriptionObjectType'
}

export interface ResponseCategory {
    id: Scalars['Int']
    name?: Scalars['String']
    href?: Scalars['String']
    productsCount?: Scalars['String']
    __typename: 'ResponseCategory'
}

export interface ResponseCompany {
    id: Scalars['Int']
    externalId?: Scalars['String']
    productCount: Scalars['Int']
    name?: Scalars['String']
    url?: Scalars['String']
    syncDate: Scalars['DateTime']
    __typename: 'ResponseCompany'
}

export interface ProductJson {
    title?: Scalars['String']
    externalId?: Scalars['String']
    url?: Scalars['String']
    currency?: Scalars['String']
    keyWords?: Scalars['String']
    description?: Scalars['String']
    price?: Scalars['String']
    priceUsd?: Scalars['String']
    scuCode?: Scalars['String']
    fullPrice?: Scalars['String']
    isDiscounted: Scalars['Boolean']
    discountPercent?: Scalars['Int']
    presence?: PresenceData
    optPrice?: Scalars['String']
    imageUrls?: (Scalars['String'] | undefined)[]
    syncDate: Scalars['DateTime']
    expirationDate: Scalars['DateTime']
    jsonCategory?: Scalars['String']
    stringCategory?: Scalars['String']
    jsonCategorySchema?: Scalars['String']
    productAttribute?: (ProductAttribute | undefined)[]
    productPaymentOptions?: (ProductPaymentOption | undefined)[]
    productDeliveryOptions?: (ProductDeliveryOption | undefined)[]
    __typename: 'ProductJson'
}

export interface PresenceData {
    presenceSureAvailable: Scalars['Boolean']
    orderAvailable: Scalars['Boolean']
    available: Scalars['Boolean']
    title?: Scalars['String']
    ending: Scalars['Boolean']
    waiting: Scalars['Boolean']
    id: Scalars['Int']
    productId: Scalars['Int']
    product?: ProductData
    __typename: 'PresenceData'
}

export interface ProductAttribute {
    id: Scalars['Int']
    productId?: Scalars['Int']
    externalId: Scalars['Int']
    attributeName?: Scalars['String']
    attributeGroup?: Scalars['String']
    attributeValues?: Scalars['String']
    product?: ProductData
    __typename: 'ProductAttribute'
}

export interface ProductPaymentOption {
    id: Scalars['Int']
    productId?: Scalars['Int']
    externalId: Scalars['Int']
    optionName?: Scalars['String']
    optionsComment?: Scalars['String']
    product?: ProductData
    __typename: 'ProductPaymentOption'
}

export interface ProductDeliveryOption {
    id: Scalars['Int']
    productId?: Scalars['Int']
    externalId: Scalars['Int']
    optionName?: Scalars['String']
    optionsComment?: Scalars['String']
    product?: ProductData
    __typename: 'ProductDeliveryOption'
}

export interface ProductData {
    id: Scalars['Int']
    companyId?: Scalars['Int']
    externalId?: Scalars['String']
    title?: Scalars['String']
    url?: Scalars['String']
    syncDate: Scalars['DateTime']
    expirationDate: Scalars['DateTime']
    productState: ProductState
    description?: Scalars['String']
    price?: Scalars['String']
    keyWords?: Scalars['String']
    jsonData?: Scalars['String']
    jsonDataSchema?: Scalars['String']
    company?: CompanyData
    presence?: PresenceData
    categories?: (CategoryData | undefined)[]
    productPaymentOptions?: (ProductPaymentOption | undefined)[]
    productDeliveryOptions?: (ProductDeliveryOption | undefined)[]
    productAttribute?: (ProductAttribute | undefined)[]
    __typename: 'ProductData'
}

export type ProductState = 'IDLE' | 'SUCCESS' | 'FAILED'

export interface CompanyData {
    id: Scalars['Int']
    sourceId?: Scalars['Int']
    externalId?: Scalars['String']
    name?: Scalars['String']
    url?: Scalars['String']
    syncDate: Scalars['DateTime']
    companyState: CompanyState
    jsonData?: Scalars['String']
    jsonDataSchema?: Scalars['String']
    products?: (ProductData | undefined)[]
    source?: CompanySource
    __typename: 'CompanyData'
}

export interface CategoryData {
    id: Scalars['Int']
    name?: Scalars['String']
    url?: Scalars['String']
    products?: (ProductData | undefined)[]
    supCategoryData?: CategoryData
    __typename: 'CategoryData'
}

export type CompanyState = 'IDLE' | 'PROCESSING' | 'SUCCESS' | 'FAILED'

export interface CompanySource {
    id: Scalars['Int']
    name?: Scalars['String']
    companies?: (CompanyData | undefined)[]
    __typename: 'CompanySource'
}

export type Query = QueryService
export type Subscription = SubscriptionObjectType

export interface QueryServiceRequest{
    categories?: [{id?: (Scalars['Int'] | null),companyId?: (Scalars['Int'] | null)},ResponseCategoryRequest] | ResponseCategoryRequest
    companies?: ResponseCompanyRequest
    products?: [{skip?: (Scalars['Int'] | null),take?: (Scalars['Int'] | null),companyId?: (Scalars['Int'] | null),categoryId?: (Scalars['Int'] | null)},ResponseProductCollectionSegmentRequest] | ResponseProductCollectionSegmentRequest
    product?: [{id: Scalars['Int']},ProductJsonRequest]
    __typename?: boolean | number
    __scalar?: boolean | number
}

export interface ResponseProductCollectionSegmentRequest{
    items?: ResponseProductRequest
    /** Information to aid in pagination. */
    pageInfo?: CollectionSegmentInfoRequest
    totalCount?: boolean | number
    __typename?: boolean | number
    __scalar?: boolean | number
}

export interface ResponseProductRequest{
    id?: boolean | number
    externalId?: boolean | number
    title?: boolean | number
    url?: boolean | number
    syncDate?: boolean | number
    description?: boolean | number
    price?: boolean | number
    __typename?: boolean | number
    __scalar?: boolean | number
}


/** Information about the offset pagination. */
export interface CollectionSegmentInfoRequest{
    /** Indicates whether more items exist following the set defined by the clients arguments. */
    hasNextPage?: boolean | number
    /** Indicates whether more items exist prior the set defined by the clients arguments. */
    hasPreviousPage?: boolean | number
    __typename?: boolean | number
    __scalar?: boolean | number
}

export interface SubscriptionObjectTypeRequest{
    subscribeProductGetDate?: ProductJsonRequest
    __typename?: boolean | number
    __scalar?: boolean | number
}

export interface ResponseCategoryRequest{
    id?: boolean | number
    name?: boolean | number
    href?: boolean | number
    productsCount?: boolean | number
    __typename?: boolean | number
    __scalar?: boolean | number
}

export interface ResponseCompanyRequest{
    id?: boolean | number
    externalId?: boolean | number
    productCount?: boolean | number
    name?: boolean | number
    url?: boolean | number
    syncDate?: boolean | number
    __typename?: boolean | number
    __scalar?: boolean | number
}

export interface ProductJsonRequest{
    title?: boolean | number
    externalId?: boolean | number
    url?: boolean | number
    currency?: boolean | number
    keyWords?: boolean | number
    description?: boolean | number
    price?: boolean | number
    priceUsd?: boolean | number
    scuCode?: boolean | number
    fullPrice?: boolean | number
    isDiscounted?: boolean | number
    discountPercent?: boolean | number
    presence?: PresenceDataRequest
    optPrice?: boolean | number
    imageUrls?: boolean | number
    syncDate?: boolean | number
    expirationDate?: boolean | number
    jsonCategory?: boolean | number
    stringCategory?: boolean | number
    jsonCategorySchema?: boolean | number
    productAttribute?: ProductAttributeRequest
    productPaymentOptions?: ProductPaymentOptionRequest
    productDeliveryOptions?: ProductDeliveryOptionRequest
    __typename?: boolean | number
    __scalar?: boolean | number
}

export interface PresenceDataRequest{
    presenceSureAvailable?: boolean | number
    orderAvailable?: boolean | number
    available?: boolean | number
    title?: boolean | number
    ending?: boolean | number
    waiting?: boolean | number
    id?: boolean | number
    productId?: boolean | number
    product?: ProductDataRequest
    __typename?: boolean | number
    __scalar?: boolean | number
}

export interface ProductAttributeRequest{
    id?: boolean | number
    productId?: boolean | number
    externalId?: boolean | number
    attributeName?: boolean | number
    attributeGroup?: boolean | number
    attributeValues?: boolean | number
    product?: ProductDataRequest
    __typename?: boolean | number
    __scalar?: boolean | number
}

export interface ProductPaymentOptionRequest{
    id?: boolean | number
    productId?: boolean | number
    externalId?: boolean | number
    optionName?: boolean | number
    optionsComment?: boolean | number
    product?: ProductDataRequest
    __typename?: boolean | number
    __scalar?: boolean | number
}

export interface ProductDeliveryOptionRequest{
    id?: boolean | number
    productId?: boolean | number
    externalId?: boolean | number
    optionName?: boolean | number
    optionsComment?: boolean | number
    product?: ProductDataRequest
    __typename?: boolean | number
    __scalar?: boolean | number
}

export interface ProductDataRequest{
    id?: boolean | number
    companyId?: boolean | number
    externalId?: boolean | number
    title?: boolean | number
    url?: boolean | number
    syncDate?: boolean | number
    expirationDate?: boolean | number
    productState?: boolean | number
    description?: boolean | number
    price?: boolean | number
    keyWords?: boolean | number
    jsonData?: boolean | number
    jsonDataSchema?: boolean | number
    company?: CompanyDataRequest
    presence?: PresenceDataRequest
    categories?: CategoryDataRequest
    productPaymentOptions?: ProductPaymentOptionRequest
    productDeliveryOptions?: ProductDeliveryOptionRequest
    productAttribute?: ProductAttributeRequest
    __typename?: boolean | number
    __scalar?: boolean | number
}

export interface CompanyDataRequest{
    id?: boolean | number
    sourceId?: boolean | number
    externalId?: boolean | number
    name?: boolean | number
    url?: boolean | number
    syncDate?: boolean | number
    companyState?: boolean | number
    jsonData?: boolean | number
    jsonDataSchema?: boolean | number
    products?: ProductDataRequest
    source?: CompanySourceRequest
    __typename?: boolean | number
    __scalar?: boolean | number
}

export interface CategoryDataRequest{
    id?: boolean | number
    name?: boolean | number
    url?: boolean | number
    products?: ProductDataRequest
    supCategoryData?: CategoryDataRequest
    __typename?: boolean | number
    __scalar?: boolean | number
}

export interface CompanySourceRequest{
    id?: boolean | number
    name?: boolean | number
    companies?: CompanyDataRequest
    __typename?: boolean | number
    __scalar?: boolean | number
}

export type QueryRequest = QueryServiceRequest
export type SubscriptionRequest = SubscriptionObjectTypeRequest


const QueryService_possibleTypes = ['QueryService']
export const isQueryService = (obj?: { __typename?: any } | null): obj is QueryService => {
  if (!obj?.__typename) throw new Error('__typename is missing in "isQueryService"')
  return QueryService_possibleTypes.includes(obj.__typename)
}



const ResponseProductCollectionSegment_possibleTypes = ['ResponseProductCollectionSegment']
export const isResponseProductCollectionSegment = (obj?: { __typename?: any } | null): obj is ResponseProductCollectionSegment => {
  if (!obj?.__typename) throw new Error('__typename is missing in "isResponseProductCollectionSegment"')
  return ResponseProductCollectionSegment_possibleTypes.includes(obj.__typename)
}



const ResponseProduct_possibleTypes = ['ResponseProduct']
export const isResponseProduct = (obj?: { __typename?: any } | null): obj is ResponseProduct => {
  if (!obj?.__typename) throw new Error('__typename is missing in "isResponseProduct"')
  return ResponseProduct_possibleTypes.includes(obj.__typename)
}



const CollectionSegmentInfo_possibleTypes = ['CollectionSegmentInfo']
export const isCollectionSegmentInfo = (obj?: { __typename?: any } | null): obj is CollectionSegmentInfo => {
  if (!obj?.__typename) throw new Error('__typename is missing in "isCollectionSegmentInfo"')
  return CollectionSegmentInfo_possibleTypes.includes(obj.__typename)
}



const SubscriptionObjectType_possibleTypes = ['SubscriptionObjectType']
export const isSubscriptionObjectType = (obj?: { __typename?: any } | null): obj is SubscriptionObjectType => {
  if (!obj?.__typename) throw new Error('__typename is missing in "isSubscriptionObjectType"')
  return SubscriptionObjectType_possibleTypes.includes(obj.__typename)
}



const ResponseCategory_possibleTypes = ['ResponseCategory']
export const isResponseCategory = (obj?: { __typename?: any } | null): obj is ResponseCategory => {
  if (!obj?.__typename) throw new Error('__typename is missing in "isResponseCategory"')
  return ResponseCategory_possibleTypes.includes(obj.__typename)
}



const ResponseCompany_possibleTypes = ['ResponseCompany']
export const isResponseCompany = (obj?: { __typename?: any } | null): obj is ResponseCompany => {
  if (!obj?.__typename) throw new Error('__typename is missing in "isResponseCompany"')
  return ResponseCompany_possibleTypes.includes(obj.__typename)
}



const ProductJson_possibleTypes = ['ProductJson']
export const isProductJson = (obj?: { __typename?: any } | null): obj is ProductJson => {
  if (!obj?.__typename) throw new Error('__typename is missing in "isProductJson"')
  return ProductJson_possibleTypes.includes(obj.__typename)
}



const PresenceData_possibleTypes = ['PresenceData']
export const isPresenceData = (obj?: { __typename?: any } | null): obj is PresenceData => {
  if (!obj?.__typename) throw new Error('__typename is missing in "isPresenceData"')
  return PresenceData_possibleTypes.includes(obj.__typename)
}



const ProductAttribute_possibleTypes = ['ProductAttribute']
export const isProductAttribute = (obj?: { __typename?: any } | null): obj is ProductAttribute => {
  if (!obj?.__typename) throw new Error('__typename is missing in "isProductAttribute"')
  return ProductAttribute_possibleTypes.includes(obj.__typename)
}



const ProductPaymentOption_possibleTypes = ['ProductPaymentOption']
export const isProductPaymentOption = (obj?: { __typename?: any } | null): obj is ProductPaymentOption => {
  if (!obj?.__typename) throw new Error('__typename is missing in "isProductPaymentOption"')
  return ProductPaymentOption_possibleTypes.includes(obj.__typename)
}



const ProductDeliveryOption_possibleTypes = ['ProductDeliveryOption']
export const isProductDeliveryOption = (obj?: { __typename?: any } | null): obj is ProductDeliveryOption => {
  if (!obj?.__typename) throw new Error('__typename is missing in "isProductDeliveryOption"')
  return ProductDeliveryOption_possibleTypes.includes(obj.__typename)
}



const ProductData_possibleTypes = ['ProductData']
export const isProductData = (obj?: { __typename?: any } | null): obj is ProductData => {
  if (!obj?.__typename) throw new Error('__typename is missing in "isProductData"')
  return ProductData_possibleTypes.includes(obj.__typename)
}



const CompanyData_possibleTypes = ['CompanyData']
export const isCompanyData = (obj?: { __typename?: any } | null): obj is CompanyData => {
  if (!obj?.__typename) throw new Error('__typename is missing in "isCompanyData"')
  return CompanyData_possibleTypes.includes(obj.__typename)
}



const CategoryData_possibleTypes = ['CategoryData']
export const isCategoryData = (obj?: { __typename?: any } | null): obj is CategoryData => {
  if (!obj?.__typename) throw new Error('__typename is missing in "isCategoryData"')
  return CategoryData_possibleTypes.includes(obj.__typename)
}



const CompanySource_possibleTypes = ['CompanySource']
export const isCompanySource = (obj?: { __typename?: any } | null): obj is CompanySource => {
  if (!obj?.__typename) throw new Error('__typename is missing in "isCompanySource"')
  return CompanySource_possibleTypes.includes(obj.__typename)
}


export interface QueryServicePromiseChain{
    categories: ((args?: {id?: (Scalars['Int'] | null),companyId?: (Scalars['Int'] | null)}) => {get: <R extends ResponseCategoryRequest>(request: R, defaultValue?: ((FieldsSelection<ResponseCategory, R> | undefined)[] | undefined)) => Promise<((FieldsSelection<ResponseCategory, R> | undefined)[] | undefined)>})&({get: <R extends ResponseCategoryRequest>(request: R, defaultValue?: ((FieldsSelection<ResponseCategory, R> | undefined)[] | undefined)) => Promise<((FieldsSelection<ResponseCategory, R> | undefined)[] | undefined)>}),
    companies: ({get: <R extends ResponseCompanyRequest>(request: R, defaultValue?: ((FieldsSelection<ResponseCompany, R> | undefined)[] | undefined)) => Promise<((FieldsSelection<ResponseCompany, R> | undefined)[] | undefined)>}),
    products: ((args?: {skip?: (Scalars['Int'] | null),take?: (Scalars['Int'] | null),companyId?: (Scalars['Int'] | null),categoryId?: (Scalars['Int'] | null)}) => ResponseProductCollectionSegmentPromiseChain & {get: <R extends ResponseProductCollectionSegmentRequest>(request: R, defaultValue?: (FieldsSelection<ResponseProductCollectionSegment, R> | undefined)) => Promise<(FieldsSelection<ResponseProductCollectionSegment, R> | undefined)>})&(ResponseProductCollectionSegmentPromiseChain & {get: <R extends ResponseProductCollectionSegmentRequest>(request: R, defaultValue?: (FieldsSelection<ResponseProductCollectionSegment, R> | undefined)) => Promise<(FieldsSelection<ResponseProductCollectionSegment, R> | undefined)>}),
    product: ((args: {id: Scalars['Int']}) => ProductJsonPromiseChain & {get: <R extends ProductJsonRequest>(request: R, defaultValue?: (FieldsSelection<ProductJson, R> | undefined)) => Promise<(FieldsSelection<ProductJson, R> | undefined)>})
}

export interface QueryServiceObservableChain{
    categories: ((args?: {id?: (Scalars['Int'] | null),companyId?: (Scalars['Int'] | null)}) => {get: <R extends ResponseCategoryRequest>(request: R, defaultValue?: ((FieldsSelection<ResponseCategory, R> | undefined)[] | undefined)) => Observable<((FieldsSelection<ResponseCategory, R> | undefined)[] | undefined)>})&({get: <R extends ResponseCategoryRequest>(request: R, defaultValue?: ((FieldsSelection<ResponseCategory, R> | undefined)[] | undefined)) => Observable<((FieldsSelection<ResponseCategory, R> | undefined)[] | undefined)>}),
    companies: ({get: <R extends ResponseCompanyRequest>(request: R, defaultValue?: ((FieldsSelection<ResponseCompany, R> | undefined)[] | undefined)) => Observable<((FieldsSelection<ResponseCompany, R> | undefined)[] | undefined)>}),
    products: ((args?: {skip?: (Scalars['Int'] | null),take?: (Scalars['Int'] | null),companyId?: (Scalars['Int'] | null),categoryId?: (Scalars['Int'] | null)}) => ResponseProductCollectionSegmentObservableChain & {get: <R extends ResponseProductCollectionSegmentRequest>(request: R, defaultValue?: (FieldsSelection<ResponseProductCollectionSegment, R> | undefined)) => Observable<(FieldsSelection<ResponseProductCollectionSegment, R> | undefined)>})&(ResponseProductCollectionSegmentObservableChain & {get: <R extends ResponseProductCollectionSegmentRequest>(request: R, defaultValue?: (FieldsSelection<ResponseProductCollectionSegment, R> | undefined)) => Observable<(FieldsSelection<ResponseProductCollectionSegment, R> | undefined)>}),
    product: ((args: {id: Scalars['Int']}) => ProductJsonObservableChain & {get: <R extends ProductJsonRequest>(request: R, defaultValue?: (FieldsSelection<ProductJson, R> | undefined)) => Observable<(FieldsSelection<ProductJson, R> | undefined)>})
}

export interface ResponseProductCollectionSegmentPromiseChain{
    items: ({get: <R extends ResponseProductRequest>(request: R, defaultValue?: ((FieldsSelection<ResponseProduct, R> | undefined)[] | undefined)) => Promise<((FieldsSelection<ResponseProduct, R> | undefined)[] | undefined)>}),
    
/** Information to aid in pagination. */
pageInfo: (CollectionSegmentInfoPromiseChain & {get: <R extends CollectionSegmentInfoRequest>(request: R, defaultValue?: FieldsSelection<CollectionSegmentInfo, R>) => Promise<FieldsSelection<CollectionSegmentInfo, R>>}),
    totalCount: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Promise<Scalars['Int']>})
}

export interface ResponseProductCollectionSegmentObservableChain{
    items: ({get: <R extends ResponseProductRequest>(request: R, defaultValue?: ((FieldsSelection<ResponseProduct, R> | undefined)[] | undefined)) => Observable<((FieldsSelection<ResponseProduct, R> | undefined)[] | undefined)>}),
    
/** Information to aid in pagination. */
pageInfo: (CollectionSegmentInfoObservableChain & {get: <R extends CollectionSegmentInfoRequest>(request: R, defaultValue?: FieldsSelection<CollectionSegmentInfo, R>) => Observable<FieldsSelection<CollectionSegmentInfo, R>>}),
    totalCount: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Observable<Scalars['Int']>})
}

export interface ResponseProductPromiseChain{
    id: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Promise<Scalars['Int']>}),
    externalId: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    title: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    url: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    syncDate: ({get: (request?: boolean|number, defaultValue?: Scalars['DateTime']) => Promise<Scalars['DateTime']>}),
    description: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    price: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>})
}

export interface ResponseProductObservableChain{
    id: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Observable<Scalars['Int']>}),
    externalId: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    title: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    url: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    syncDate: ({get: (request?: boolean|number, defaultValue?: Scalars['DateTime']) => Observable<Scalars['DateTime']>}),
    description: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    price: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>})
}


/** Information about the offset pagination. */
export interface CollectionSegmentInfoPromiseChain{
    
/** Indicates whether more items exist following the set defined by the clients arguments. */
hasNextPage: ({get: (request?: boolean|number, defaultValue?: Scalars['Boolean']) => Promise<Scalars['Boolean']>}),
    
/** Indicates whether more items exist prior the set defined by the clients arguments. */
hasPreviousPage: ({get: (request?: boolean|number, defaultValue?: Scalars['Boolean']) => Promise<Scalars['Boolean']>})
}


/** Information about the offset pagination. */
export interface CollectionSegmentInfoObservableChain{
    
/** Indicates whether more items exist following the set defined by the clients arguments. */
hasNextPage: ({get: (request?: boolean|number, defaultValue?: Scalars['Boolean']) => Observable<Scalars['Boolean']>}),
    
/** Indicates whether more items exist prior the set defined by the clients arguments. */
hasPreviousPage: ({get: (request?: boolean|number, defaultValue?: Scalars['Boolean']) => Observable<Scalars['Boolean']>})
}

export interface SubscriptionObjectTypePromiseChain{
    subscribeProductGetDate: (ProductJsonPromiseChain & {get: <R extends ProductJsonRequest>(request: R, defaultValue?: (FieldsSelection<ProductJson, R> | undefined)) => Promise<(FieldsSelection<ProductJson, R> | undefined)>})
}

export interface SubscriptionObjectTypeObservableChain{
    subscribeProductGetDate: (ProductJsonObservableChain & {get: <R extends ProductJsonRequest>(request: R, defaultValue?: (FieldsSelection<ProductJson, R> | undefined)) => Observable<(FieldsSelection<ProductJson, R> | undefined)>})
}

export interface ResponseCategoryPromiseChain{
    id: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Promise<Scalars['Int']>}),
    name: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    href: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    productsCount: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>})
}

export interface ResponseCategoryObservableChain{
    id: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Observable<Scalars['Int']>}),
    name: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    href: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    productsCount: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>})
}

export interface ResponseCompanyPromiseChain{
    id: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Promise<Scalars['Int']>}),
    externalId: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    productCount: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Promise<Scalars['Int']>}),
    name: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    url: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    syncDate: ({get: (request?: boolean|number, defaultValue?: Scalars['DateTime']) => Promise<Scalars['DateTime']>})
}

export interface ResponseCompanyObservableChain{
    id: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Observable<Scalars['Int']>}),
    externalId: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    productCount: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Observable<Scalars['Int']>}),
    name: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    url: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    syncDate: ({get: (request?: boolean|number, defaultValue?: Scalars['DateTime']) => Observable<Scalars['DateTime']>})
}

export interface ProductJsonPromiseChain{
    title: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    externalId: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    url: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    currency: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    keyWords: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    description: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    price: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    priceUsd: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    scuCode: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    fullPrice: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    isDiscounted: ({get: (request?: boolean|number, defaultValue?: Scalars['Boolean']) => Promise<Scalars['Boolean']>}),
    discountPercent: ({get: (request?: boolean|number, defaultValue?: (Scalars['Int'] | undefined)) => Promise<(Scalars['Int'] | undefined)>}),
    presence: (PresenceDataPromiseChain & {get: <R extends PresenceDataRequest>(request: R, defaultValue?: (FieldsSelection<PresenceData, R> | undefined)) => Promise<(FieldsSelection<PresenceData, R> | undefined)>}),
    optPrice: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    imageUrls: ({get: (request?: boolean|number, defaultValue?: ((Scalars['String'] | undefined)[] | undefined)) => Promise<((Scalars['String'] | undefined)[] | undefined)>}),
    syncDate: ({get: (request?: boolean|number, defaultValue?: Scalars['DateTime']) => Promise<Scalars['DateTime']>}),
    expirationDate: ({get: (request?: boolean|number, defaultValue?: Scalars['DateTime']) => Promise<Scalars['DateTime']>}),
    jsonCategory: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    stringCategory: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    jsonCategorySchema: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    productAttribute: ({get: <R extends ProductAttributeRequest>(request: R, defaultValue?: ((FieldsSelection<ProductAttribute, R> | undefined)[] | undefined)) => Promise<((FieldsSelection<ProductAttribute, R> | undefined)[] | undefined)>}),
    productPaymentOptions: ({get: <R extends ProductPaymentOptionRequest>(request: R, defaultValue?: ((FieldsSelection<ProductPaymentOption, R> | undefined)[] | undefined)) => Promise<((FieldsSelection<ProductPaymentOption, R> | undefined)[] | undefined)>}),
    productDeliveryOptions: ({get: <R extends ProductDeliveryOptionRequest>(request: R, defaultValue?: ((FieldsSelection<ProductDeliveryOption, R> | undefined)[] | undefined)) => Promise<((FieldsSelection<ProductDeliveryOption, R> | undefined)[] | undefined)>})
}

export interface ProductJsonObservableChain{
    title: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    externalId: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    url: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    currency: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    keyWords: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    description: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    price: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    priceUsd: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    scuCode: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    fullPrice: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    isDiscounted: ({get: (request?: boolean|number, defaultValue?: Scalars['Boolean']) => Observable<Scalars['Boolean']>}),
    discountPercent: ({get: (request?: boolean|number, defaultValue?: (Scalars['Int'] | undefined)) => Observable<(Scalars['Int'] | undefined)>}),
    presence: (PresenceDataObservableChain & {get: <R extends PresenceDataRequest>(request: R, defaultValue?: (FieldsSelection<PresenceData, R> | undefined)) => Observable<(FieldsSelection<PresenceData, R> | undefined)>}),
    optPrice: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    imageUrls: ({get: (request?: boolean|number, defaultValue?: ((Scalars['String'] | undefined)[] | undefined)) => Observable<((Scalars['String'] | undefined)[] | undefined)>}),
    syncDate: ({get: (request?: boolean|number, defaultValue?: Scalars['DateTime']) => Observable<Scalars['DateTime']>}),
    expirationDate: ({get: (request?: boolean|number, defaultValue?: Scalars['DateTime']) => Observable<Scalars['DateTime']>}),
    jsonCategory: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    stringCategory: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    jsonCategorySchema: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    productAttribute: ({get: <R extends ProductAttributeRequest>(request: R, defaultValue?: ((FieldsSelection<ProductAttribute, R> | undefined)[] | undefined)) => Observable<((FieldsSelection<ProductAttribute, R> | undefined)[] | undefined)>}),
    productPaymentOptions: ({get: <R extends ProductPaymentOptionRequest>(request: R, defaultValue?: ((FieldsSelection<ProductPaymentOption, R> | undefined)[] | undefined)) => Observable<((FieldsSelection<ProductPaymentOption, R> | undefined)[] | undefined)>}),
    productDeliveryOptions: ({get: <R extends ProductDeliveryOptionRequest>(request: R, defaultValue?: ((FieldsSelection<ProductDeliveryOption, R> | undefined)[] | undefined)) => Observable<((FieldsSelection<ProductDeliveryOption, R> | undefined)[] | undefined)>})
}

export interface PresenceDataPromiseChain{
    presenceSureAvailable: ({get: (request?: boolean|number, defaultValue?: Scalars['Boolean']) => Promise<Scalars['Boolean']>}),
    orderAvailable: ({get: (request?: boolean|number, defaultValue?: Scalars['Boolean']) => Promise<Scalars['Boolean']>}),
    available: ({get: (request?: boolean|number, defaultValue?: Scalars['Boolean']) => Promise<Scalars['Boolean']>}),
    title: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    ending: ({get: (request?: boolean|number, defaultValue?: Scalars['Boolean']) => Promise<Scalars['Boolean']>}),
    waiting: ({get: (request?: boolean|number, defaultValue?: Scalars['Boolean']) => Promise<Scalars['Boolean']>}),
    id: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Promise<Scalars['Int']>}),
    productId: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Promise<Scalars['Int']>}),
    product: (ProductDataPromiseChain & {get: <R extends ProductDataRequest>(request: R, defaultValue?: (FieldsSelection<ProductData, R> | undefined)) => Promise<(FieldsSelection<ProductData, R> | undefined)>})
}

export interface PresenceDataObservableChain{
    presenceSureAvailable: ({get: (request?: boolean|number, defaultValue?: Scalars['Boolean']) => Observable<Scalars['Boolean']>}),
    orderAvailable: ({get: (request?: boolean|number, defaultValue?: Scalars['Boolean']) => Observable<Scalars['Boolean']>}),
    available: ({get: (request?: boolean|number, defaultValue?: Scalars['Boolean']) => Observable<Scalars['Boolean']>}),
    title: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    ending: ({get: (request?: boolean|number, defaultValue?: Scalars['Boolean']) => Observable<Scalars['Boolean']>}),
    waiting: ({get: (request?: boolean|number, defaultValue?: Scalars['Boolean']) => Observable<Scalars['Boolean']>}),
    id: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Observable<Scalars['Int']>}),
    productId: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Observable<Scalars['Int']>}),
    product: (ProductDataObservableChain & {get: <R extends ProductDataRequest>(request: R, defaultValue?: (FieldsSelection<ProductData, R> | undefined)) => Observable<(FieldsSelection<ProductData, R> | undefined)>})
}

export interface ProductAttributePromiseChain{
    id: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Promise<Scalars['Int']>}),
    productId: ({get: (request?: boolean|number, defaultValue?: (Scalars['Int'] | undefined)) => Promise<(Scalars['Int'] | undefined)>}),
    externalId: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Promise<Scalars['Int']>}),
    attributeName: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    attributeGroup: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    attributeValues: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    product: (ProductDataPromiseChain & {get: <R extends ProductDataRequest>(request: R, defaultValue?: (FieldsSelection<ProductData, R> | undefined)) => Promise<(FieldsSelection<ProductData, R> | undefined)>})
}

export interface ProductAttributeObservableChain{
    id: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Observable<Scalars['Int']>}),
    productId: ({get: (request?: boolean|number, defaultValue?: (Scalars['Int'] | undefined)) => Observable<(Scalars['Int'] | undefined)>}),
    externalId: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Observable<Scalars['Int']>}),
    attributeName: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    attributeGroup: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    attributeValues: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    product: (ProductDataObservableChain & {get: <R extends ProductDataRequest>(request: R, defaultValue?: (FieldsSelection<ProductData, R> | undefined)) => Observable<(FieldsSelection<ProductData, R> | undefined)>})
}

export interface ProductPaymentOptionPromiseChain{
    id: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Promise<Scalars['Int']>}),
    productId: ({get: (request?: boolean|number, defaultValue?: (Scalars['Int'] | undefined)) => Promise<(Scalars['Int'] | undefined)>}),
    externalId: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Promise<Scalars['Int']>}),
    optionName: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    optionsComment: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    product: (ProductDataPromiseChain & {get: <R extends ProductDataRequest>(request: R, defaultValue?: (FieldsSelection<ProductData, R> | undefined)) => Promise<(FieldsSelection<ProductData, R> | undefined)>})
}

export interface ProductPaymentOptionObservableChain{
    id: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Observable<Scalars['Int']>}),
    productId: ({get: (request?: boolean|number, defaultValue?: (Scalars['Int'] | undefined)) => Observable<(Scalars['Int'] | undefined)>}),
    externalId: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Observable<Scalars['Int']>}),
    optionName: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    optionsComment: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    product: (ProductDataObservableChain & {get: <R extends ProductDataRequest>(request: R, defaultValue?: (FieldsSelection<ProductData, R> | undefined)) => Observable<(FieldsSelection<ProductData, R> | undefined)>})
}

export interface ProductDeliveryOptionPromiseChain{
    id: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Promise<Scalars['Int']>}),
    productId: ({get: (request?: boolean|number, defaultValue?: (Scalars['Int'] | undefined)) => Promise<(Scalars['Int'] | undefined)>}),
    externalId: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Promise<Scalars['Int']>}),
    optionName: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    optionsComment: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    product: (ProductDataPromiseChain & {get: <R extends ProductDataRequest>(request: R, defaultValue?: (FieldsSelection<ProductData, R> | undefined)) => Promise<(FieldsSelection<ProductData, R> | undefined)>})
}

export interface ProductDeliveryOptionObservableChain{
    id: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Observable<Scalars['Int']>}),
    productId: ({get: (request?: boolean|number, defaultValue?: (Scalars['Int'] | undefined)) => Observable<(Scalars['Int'] | undefined)>}),
    externalId: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Observable<Scalars['Int']>}),
    optionName: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    optionsComment: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    product: (ProductDataObservableChain & {get: <R extends ProductDataRequest>(request: R, defaultValue?: (FieldsSelection<ProductData, R> | undefined)) => Observable<(FieldsSelection<ProductData, R> | undefined)>})
}

export interface ProductDataPromiseChain{
    id: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Promise<Scalars['Int']>}),
    companyId: ({get: (request?: boolean|number, defaultValue?: (Scalars['Int'] | undefined)) => Promise<(Scalars['Int'] | undefined)>}),
    externalId: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    title: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    url: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    syncDate: ({get: (request?: boolean|number, defaultValue?: Scalars['DateTime']) => Promise<Scalars['DateTime']>}),
    expirationDate: ({get: (request?: boolean|number, defaultValue?: Scalars['DateTime']) => Promise<Scalars['DateTime']>}),
    productState: ({get: (request?: boolean|number, defaultValue?: ProductState) => Promise<ProductState>}),
    description: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    price: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    keyWords: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    jsonData: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    jsonDataSchema: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    company: (CompanyDataPromiseChain & {get: <R extends CompanyDataRequest>(request: R, defaultValue?: (FieldsSelection<CompanyData, R> | undefined)) => Promise<(FieldsSelection<CompanyData, R> | undefined)>}),
    presence: (PresenceDataPromiseChain & {get: <R extends PresenceDataRequest>(request: R, defaultValue?: (FieldsSelection<PresenceData, R> | undefined)) => Promise<(FieldsSelection<PresenceData, R> | undefined)>}),
    categories: ({get: <R extends CategoryDataRequest>(request: R, defaultValue?: ((FieldsSelection<CategoryData, R> | undefined)[] | undefined)) => Promise<((FieldsSelection<CategoryData, R> | undefined)[] | undefined)>}),
    productPaymentOptions: ({get: <R extends ProductPaymentOptionRequest>(request: R, defaultValue?: ((FieldsSelection<ProductPaymentOption, R> | undefined)[] | undefined)) => Promise<((FieldsSelection<ProductPaymentOption, R> | undefined)[] | undefined)>}),
    productDeliveryOptions: ({get: <R extends ProductDeliveryOptionRequest>(request: R, defaultValue?: ((FieldsSelection<ProductDeliveryOption, R> | undefined)[] | undefined)) => Promise<((FieldsSelection<ProductDeliveryOption, R> | undefined)[] | undefined)>}),
    productAttribute: ({get: <R extends ProductAttributeRequest>(request: R, defaultValue?: ((FieldsSelection<ProductAttribute, R> | undefined)[] | undefined)) => Promise<((FieldsSelection<ProductAttribute, R> | undefined)[] | undefined)>})
}

export interface ProductDataObservableChain{
    id: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Observable<Scalars['Int']>}),
    companyId: ({get: (request?: boolean|number, defaultValue?: (Scalars['Int'] | undefined)) => Observable<(Scalars['Int'] | undefined)>}),
    externalId: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    title: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    url: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    syncDate: ({get: (request?: boolean|number, defaultValue?: Scalars['DateTime']) => Observable<Scalars['DateTime']>}),
    expirationDate: ({get: (request?: boolean|number, defaultValue?: Scalars['DateTime']) => Observable<Scalars['DateTime']>}),
    productState: ({get: (request?: boolean|number, defaultValue?: ProductState) => Observable<ProductState>}),
    description: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    price: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    keyWords: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    jsonData: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    jsonDataSchema: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    company: (CompanyDataObservableChain & {get: <R extends CompanyDataRequest>(request: R, defaultValue?: (FieldsSelection<CompanyData, R> | undefined)) => Observable<(FieldsSelection<CompanyData, R> | undefined)>}),
    presence: (PresenceDataObservableChain & {get: <R extends PresenceDataRequest>(request: R, defaultValue?: (FieldsSelection<PresenceData, R> | undefined)) => Observable<(FieldsSelection<PresenceData, R> | undefined)>}),
    categories: ({get: <R extends CategoryDataRequest>(request: R, defaultValue?: ((FieldsSelection<CategoryData, R> | undefined)[] | undefined)) => Observable<((FieldsSelection<CategoryData, R> | undefined)[] | undefined)>}),
    productPaymentOptions: ({get: <R extends ProductPaymentOptionRequest>(request: R, defaultValue?: ((FieldsSelection<ProductPaymentOption, R> | undefined)[] | undefined)) => Observable<((FieldsSelection<ProductPaymentOption, R> | undefined)[] | undefined)>}),
    productDeliveryOptions: ({get: <R extends ProductDeliveryOptionRequest>(request: R, defaultValue?: ((FieldsSelection<ProductDeliveryOption, R> | undefined)[] | undefined)) => Observable<((FieldsSelection<ProductDeliveryOption, R> | undefined)[] | undefined)>}),
    productAttribute: ({get: <R extends ProductAttributeRequest>(request: R, defaultValue?: ((FieldsSelection<ProductAttribute, R> | undefined)[] | undefined)) => Observable<((FieldsSelection<ProductAttribute, R> | undefined)[] | undefined)>})
}

export interface CompanyDataPromiseChain{
    id: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Promise<Scalars['Int']>}),
    sourceId: ({get: (request?: boolean|number, defaultValue?: (Scalars['Int'] | undefined)) => Promise<(Scalars['Int'] | undefined)>}),
    externalId: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    name: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    url: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    syncDate: ({get: (request?: boolean|number, defaultValue?: Scalars['DateTime']) => Promise<Scalars['DateTime']>}),
    companyState: ({get: (request?: boolean|number, defaultValue?: CompanyState) => Promise<CompanyState>}),
    jsonData: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    jsonDataSchema: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    products: ({get: <R extends ProductDataRequest>(request: R, defaultValue?: ((FieldsSelection<ProductData, R> | undefined)[] | undefined)) => Promise<((FieldsSelection<ProductData, R> | undefined)[] | undefined)>}),
    source: (CompanySourcePromiseChain & {get: <R extends CompanySourceRequest>(request: R, defaultValue?: (FieldsSelection<CompanySource, R> | undefined)) => Promise<(FieldsSelection<CompanySource, R> | undefined)>})
}

export interface CompanyDataObservableChain{
    id: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Observable<Scalars['Int']>}),
    sourceId: ({get: (request?: boolean|number, defaultValue?: (Scalars['Int'] | undefined)) => Observable<(Scalars['Int'] | undefined)>}),
    externalId: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    name: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    url: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    syncDate: ({get: (request?: boolean|number, defaultValue?: Scalars['DateTime']) => Observable<Scalars['DateTime']>}),
    companyState: ({get: (request?: boolean|number, defaultValue?: CompanyState) => Observable<CompanyState>}),
    jsonData: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    jsonDataSchema: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    products: ({get: <R extends ProductDataRequest>(request: R, defaultValue?: ((FieldsSelection<ProductData, R> | undefined)[] | undefined)) => Observable<((FieldsSelection<ProductData, R> | undefined)[] | undefined)>}),
    source: (CompanySourceObservableChain & {get: <R extends CompanySourceRequest>(request: R, defaultValue?: (FieldsSelection<CompanySource, R> | undefined)) => Observable<(FieldsSelection<CompanySource, R> | undefined)>})
}

export interface CategoryDataPromiseChain{
    id: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Promise<Scalars['Int']>}),
    name: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    url: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    products: ({get: <R extends ProductDataRequest>(request: R, defaultValue?: ((FieldsSelection<ProductData, R> | undefined)[] | undefined)) => Promise<((FieldsSelection<ProductData, R> | undefined)[] | undefined)>}),
    supCategoryData: (CategoryDataPromiseChain & {get: <R extends CategoryDataRequest>(request: R, defaultValue?: (FieldsSelection<CategoryData, R> | undefined)) => Promise<(FieldsSelection<CategoryData, R> | undefined)>})
}

export interface CategoryDataObservableChain{
    id: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Observable<Scalars['Int']>}),
    name: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    url: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    products: ({get: <R extends ProductDataRequest>(request: R, defaultValue?: ((FieldsSelection<ProductData, R> | undefined)[] | undefined)) => Observable<((FieldsSelection<ProductData, R> | undefined)[] | undefined)>}),
    supCategoryData: (CategoryDataObservableChain & {get: <R extends CategoryDataRequest>(request: R, defaultValue?: (FieldsSelection<CategoryData, R> | undefined)) => Observable<(FieldsSelection<CategoryData, R> | undefined)>})
}

export interface CompanySourcePromiseChain{
    id: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Promise<Scalars['Int']>}),
    name: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Promise<(Scalars['String'] | undefined)>}),
    companies: ({get: <R extends CompanyDataRequest>(request: R, defaultValue?: ((FieldsSelection<CompanyData, R> | undefined)[] | undefined)) => Promise<((FieldsSelection<CompanyData, R> | undefined)[] | undefined)>})
}

export interface CompanySourceObservableChain{
    id: ({get: (request?: boolean|number, defaultValue?: Scalars['Int']) => Observable<Scalars['Int']>}),
    name: ({get: (request?: boolean|number, defaultValue?: (Scalars['String'] | undefined)) => Observable<(Scalars['String'] | undefined)>}),
    companies: ({get: <R extends CompanyDataRequest>(request: R, defaultValue?: ((FieldsSelection<CompanyData, R> | undefined)[] | undefined)) => Observable<((FieldsSelection<CompanyData, R> | undefined)[] | undefined)>})
}