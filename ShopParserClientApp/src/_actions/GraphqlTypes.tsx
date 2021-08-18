import { gql } from '@apollo/client';
export type Maybe<T> = T | null;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> };
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> };
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: string;
  String: string;
  Boolean: boolean;
  Int: number;
  Float: number;
  /** The `DateTime` scalar represents an ISO-8601 compliant date time type. */
  DateTime: any;
};



export type CategoryData = {
  __typename?: 'CategoryData';
  id: Scalars['Int'];
  name?: Maybe<Scalars['String']>;
  url?: Maybe<Scalars['String']>;
  products?: Maybe<Array<Maybe<ProductData>>>;
  supCategoryData?: Maybe<CategoryData>;
};

/** Information about the offset pagination. */
export type CollectionSegmentInfo = {
  __typename?: 'CollectionSegmentInfo';
  /** Indicates whether more items exist following the set defined by the clients arguments. */
  hasNextPage: Scalars['Boolean'];
  /** Indicates whether more items exist prior the set defined by the clients arguments. */
  hasPreviousPage: Scalars['Boolean'];
};

export type CompanyData = {
  __typename?: 'CompanyData';
  id: Scalars['Int'];
  sourceId?: Maybe<Scalars['Int']>;
  externalId?: Maybe<Scalars['String']>;
  name?: Maybe<Scalars['String']>;
  url?: Maybe<Scalars['String']>;
  syncDate: Scalars['DateTime'];
  companyState: CompanyState;
  jsonData?: Maybe<Scalars['String']>;
  jsonDataSchema?: Maybe<Scalars['String']>;
  products?: Maybe<Array<Maybe<ProductData>>>;
  source?: Maybe<CompanySource>;
};

export type CompanySource = {
  __typename?: 'CompanySource';
  id: Scalars['Int'];
  name?: Maybe<Scalars['String']>;
  companies?: Maybe<Array<Maybe<CompanyData>>>;
};

export enum CompanyState {
  Idle = 'IDLE',
  Processing = 'PROCESSING',
  Success = 'SUCCESS',
  Failed = 'FAILED'
}


export type PresenceData = {
  __typename?: 'PresenceData';
  presenceSureAvailable: Scalars['Boolean'];
  orderAvailable: Scalars['Boolean'];
  available: Scalars['Boolean'];
  title?: Maybe<Scalars['String']>;
  ending: Scalars['Boolean'];
  waiting: Scalars['Boolean'];
  id: Scalars['Int'];
  productId: Scalars['Int'];
  product?: Maybe<ProductData>;
};

export type ProductAttribute = {
  __typename?: 'ProductAttribute';
  id: Scalars['Int'];
  productId?: Maybe<Scalars['Int']>;
  externalId: Scalars['Int'];
  attributeName?: Maybe<Scalars['String']>;
  attributeGroup?: Maybe<Scalars['String']>;
  attributeValues?: Maybe<Scalars['String']>;
  product?: Maybe<ProductData>;
};

export type ProductData = {
  __typename?: 'ProductData';
  id: Scalars['Int'];
  companyId?: Maybe<Scalars['Int']>;
  externalId?: Maybe<Scalars['String']>;
  title?: Maybe<Scalars['String']>;
  url?: Maybe<Scalars['String']>;
  syncDate: Scalars['DateTime'];
  expirationDate: Scalars['DateTime'];
  productState: ProductState;
  description?: Maybe<Scalars['String']>;
  price?: Maybe<Scalars['String']>;
  keyWords?: Maybe<Scalars['String']>;
  jsonData?: Maybe<Scalars['String']>;
  jsonDataSchema?: Maybe<Scalars['String']>;
  company?: Maybe<CompanyData>;
  presence?: Maybe<PresenceData>;
  categories?: Maybe<Array<Maybe<CategoryData>>>;
  productPaymentOptions?: Maybe<Array<Maybe<ProductPaymentOption>>>;
  productDeliveryOptions?: Maybe<Array<Maybe<ProductDeliveryOption>>>;
  productAttribute?: Maybe<Array<Maybe<ProductAttribute>>>;
};

export type ProductDeliveryOption = {
  __typename?: 'ProductDeliveryOption';
  id: Scalars['Int'];
  productId?: Maybe<Scalars['Int']>;
  externalId: Scalars['Int'];
  optionName?: Maybe<Scalars['String']>;
  optionsComment?: Maybe<Scalars['String']>;
  product?: Maybe<ProductData>;
};

export type ProductJson = {
  __typename?: 'ProductJson';
  title?: Maybe<Scalars['String']>;
  externalId?: Maybe<Scalars['String']>;
  url?: Maybe<Scalars['String']>;
  currency?: Maybe<Scalars['String']>;
  keyWords?: Maybe<Scalars['String']>;
  description?: Maybe<Scalars['String']>;
  price?: Maybe<Scalars['String']>;
  priceUsd?: Maybe<Scalars['String']>;
  scuCode?: Maybe<Scalars['String']>;
  fullPrice?: Maybe<Scalars['String']>;
  isDiscounted: Scalars['Boolean'];
  discountPercent?: Maybe<Scalars['Int']>;
  presence?: Maybe<PresenceData>;
  optPrice?: Maybe<Scalars['String']>;
  imageUrls?: Maybe<Array<Maybe<Scalars['String']>>>;
  syncDate: Scalars['DateTime'];
  expirationDate: Scalars['DateTime'];
  jsonCategory?: Maybe<Scalars['String']>;
  stringCategory?: Maybe<Scalars['String']>;
  jsonCategorySchema?: Maybe<Scalars['String']>;
  productAttribute?: Maybe<Array<Maybe<ProductAttribute>>>;
  productPaymentOptions?: Maybe<Array<Maybe<ProductPaymentOption>>>;
  productDeliveryOptions?: Maybe<Array<Maybe<ProductDeliveryOption>>>;
};

export type ProductPaymentOption = {
  __typename?: 'ProductPaymentOption';
  id: Scalars['Int'];
  productId?: Maybe<Scalars['Int']>;
  externalId: Scalars['Int'];
  optionName?: Maybe<Scalars['String']>;
  optionsComment?: Maybe<Scalars['String']>;
  product?: Maybe<ProductData>;
};

export enum ProductState {
  Idle = 'IDLE',
  Success = 'SUCCESS',
  Failed = 'FAILED'
}

export type QueryService = {
  __typename?: 'QueryService';
  categories?: Maybe<Array<Maybe<ResponseCategory>>>;
  companies?: Maybe<Array<Maybe<ResponseCompany>>>;
  products?: Maybe<ResponseProductCollectionSegment>;
  product?: Maybe<ProductJson>;
};


export type QueryServiceCategoriesArgs = {
  id?: Maybe<Scalars['Int']>;
  companyId?: Maybe<Scalars['Int']>;
};


export type QueryServiceProductsArgs = {
  skip?: Maybe<Scalars['Int']>;
  take?: Maybe<Scalars['Int']>;
  companyId?: Maybe<Scalars['Int']>;
  categoryId?: Maybe<Scalars['Int']>;
};


export type QueryServiceProductArgs = {
  id: Scalars['Int'];
};

export type ResponseCategory = {
  __typename?: 'ResponseCategory';
  id: Scalars['Int'];
  name?: Maybe<Scalars['String']>;
  href?: Maybe<Scalars['String']>;
  productsCount?: Maybe<Scalars['String']>;
};

export type ResponseCompany = {
  __typename?: 'ResponseCompany';
  id: Scalars['Int'];
  externalId?: Maybe<Scalars['String']>;
  productCount: Scalars['Int'];
  name?: Maybe<Scalars['String']>;
  url?: Maybe<Scalars['String']>;
  syncDate: Scalars['DateTime'];
};

export type ResponseProduct = {
  __typename?: 'ResponseProduct';
  id: Scalars['Int'];
  externalId?: Maybe<Scalars['String']>;
  title?: Maybe<Scalars['String']>;
  url?: Maybe<Scalars['String']>;
  syncDate: Scalars['DateTime'];
  description?: Maybe<Scalars['String']>;
  price?: Maybe<Scalars['String']>;
};

export type ResponseProductCollectionSegment = {
  __typename?: 'ResponseProductCollectionSegment';
  items?: Maybe<Array<Maybe<ResponseProduct>>>;
  /** Information to aid in pagination. */
  pageInfo: CollectionSegmentInfo;
  totalCount: Scalars['Int'];
};
