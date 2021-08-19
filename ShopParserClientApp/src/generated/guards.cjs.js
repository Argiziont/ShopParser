
var QueryService_possibleTypes = ['QueryService']
module.exports.isQueryService = function(obj) {
  if (!obj || !obj.__typename) throw new Error('__typename is missing in "isQueryService"')
  return QueryService_possibleTypes.includes(obj.__typename)
}



var ResponseProductCollectionSegment_possibleTypes = ['ResponseProductCollectionSegment']
module.exports.isResponseProductCollectionSegment = function(obj) {
  if (!obj || !obj.__typename) throw new Error('__typename is missing in "isResponseProductCollectionSegment"')
  return ResponseProductCollectionSegment_possibleTypes.includes(obj.__typename)
}



var ResponseProduct_possibleTypes = ['ResponseProduct']
module.exports.isResponseProduct = function(obj) {
  if (!obj || !obj.__typename) throw new Error('__typename is missing in "isResponseProduct"')
  return ResponseProduct_possibleTypes.includes(obj.__typename)
}



var CollectionSegmentInfo_possibleTypes = ['CollectionSegmentInfo']
module.exports.isCollectionSegmentInfo = function(obj) {
  if (!obj || !obj.__typename) throw new Error('__typename is missing in "isCollectionSegmentInfo"')
  return CollectionSegmentInfo_possibleTypes.includes(obj.__typename)
}



var SubscriptionObjectType_possibleTypes = ['SubscriptionObjectType']
module.exports.isSubscriptionObjectType = function(obj) {
  if (!obj || !obj.__typename) throw new Error('__typename is missing in "isSubscriptionObjectType"')
  return SubscriptionObjectType_possibleTypes.includes(obj.__typename)
}



var ResponseCategory_possibleTypes = ['ResponseCategory']
module.exports.isResponseCategory = function(obj) {
  if (!obj || !obj.__typename) throw new Error('__typename is missing in "isResponseCategory"')
  return ResponseCategory_possibleTypes.includes(obj.__typename)
}



var ResponseCompany_possibleTypes = ['ResponseCompany']
module.exports.isResponseCompany = function(obj) {
  if (!obj || !obj.__typename) throw new Error('__typename is missing in "isResponseCompany"')
  return ResponseCompany_possibleTypes.includes(obj.__typename)
}



var ProductJson_possibleTypes = ['ProductJson']
module.exports.isProductJson = function(obj) {
  if (!obj || !obj.__typename) throw new Error('__typename is missing in "isProductJson"')
  return ProductJson_possibleTypes.includes(obj.__typename)
}



var PresenceData_possibleTypes = ['PresenceData']
module.exports.isPresenceData = function(obj) {
  if (!obj || !obj.__typename) throw new Error('__typename is missing in "isPresenceData"')
  return PresenceData_possibleTypes.includes(obj.__typename)
}



var ProductAttribute_possibleTypes = ['ProductAttribute']
module.exports.isProductAttribute = function(obj) {
  if (!obj || !obj.__typename) throw new Error('__typename is missing in "isProductAttribute"')
  return ProductAttribute_possibleTypes.includes(obj.__typename)
}



var ProductPaymentOption_possibleTypes = ['ProductPaymentOption']
module.exports.isProductPaymentOption = function(obj) {
  if (!obj || !obj.__typename) throw new Error('__typename is missing in "isProductPaymentOption"')
  return ProductPaymentOption_possibleTypes.includes(obj.__typename)
}



var ProductDeliveryOption_possibleTypes = ['ProductDeliveryOption']
module.exports.isProductDeliveryOption = function(obj) {
  if (!obj || !obj.__typename) throw new Error('__typename is missing in "isProductDeliveryOption"')
  return ProductDeliveryOption_possibleTypes.includes(obj.__typename)
}



var ProductData_possibleTypes = ['ProductData']
module.exports.isProductData = function(obj) {
  if (!obj || !obj.__typename) throw new Error('__typename is missing in "isProductData"')
  return ProductData_possibleTypes.includes(obj.__typename)
}



var CompanyData_possibleTypes = ['CompanyData']
module.exports.isCompanyData = function(obj) {
  if (!obj || !obj.__typename) throw new Error('__typename is missing in "isCompanyData"')
  return CompanyData_possibleTypes.includes(obj.__typename)
}



var CategoryData_possibleTypes = ['CategoryData']
module.exports.isCategoryData = function(obj) {
  if (!obj || !obj.__typename) throw new Error('__typename is missing in "isCategoryData"')
  return CategoryData_possibleTypes.includes(obj.__typename)
}



var CompanySource_possibleTypes = ['CompanySource']
module.exports.isCompanySource = function(obj) {
  if (!obj || !obj.__typename) throw new Error('__typename is missing in "isCompanySource"')
  return CompanySource_possibleTypes.includes(obj.__typename)
}
