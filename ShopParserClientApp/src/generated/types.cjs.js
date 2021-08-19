module.exports = {
    "scalars": [
        0,
        1,
        2,
        9,
        15,
        18
    ],
    "types": {
        "String": {},
        "Boolean": {},
        "Int": {},
        "ResponseProductCollectionSegment": {
            "items": [
                4
            ],
            "pageInfo": [
                5
            ],
            "totalCount": [
                2
            ],
            "__typename": [
                0
            ]
        },
        "ResponseProduct": {
            "id": [
                2
            ],
            "externalId": [
                0
            ],
            "title": [
                0
            ],
            "url": [
                0
            ],
            "syncDate": [
                9
            ],
            "description": [
                0
            ],
            "price": [
                0
            ],
            "__typename": [
                0
            ]
        },
        "CollectionSegmentInfo": {
            "hasNextPage": [
                1
            ],
            "hasPreviousPage": [
                1
            ],
            "__typename": [
                0
            ]
        },
        "ResponseCategory": {
            "id": [
                2
            ],
            "name": [
                0
            ],
            "href": [
                0
            ],
            "productsCount": [
                0
            ],
            "__typename": [
                0
            ]
        },
        "ResponseCompany": {
            "id": [
                2
            ],
            "externalId": [
                0
            ],
            "productCount": [
                2
            ],
            "name": [
                0
            ],
            "url": [
                0
            ],
            "syncDate": [
                9
            ],
            "__typename": [
                0
            ]
        },
        "ProductJson": {
            "title": [
                0
            ],
            "externalId": [
                0
            ],
            "url": [
                0
            ],
            "currency": [
                0
            ],
            "keyWords": [
                0
            ],
            "description": [
                0
            ],
            "price": [
                0
            ],
            "priceUsd": [
                0
            ],
            "scuCode": [
                0
            ],
            "fullPrice": [
                0
            ],
            "isDiscounted": [
                1
            ],
            "discountPercent": [
                2
            ],
            "presence": [
                10
            ],
            "optPrice": [
                0
            ],
            "imageUrls": [
                0
            ],
            "syncDate": [
                9
            ],
            "expirationDate": [
                9
            ],
            "jsonCategory": [
                0
            ],
            "stringCategory": [
                0
            ],
            "jsonCategorySchema": [
                0
            ],
            "productAttribute": [
                11
            ],
            "productPaymentOptions": [
                12
            ],
            "productDeliveryOptions": [
                13
            ],
            "__typename": [
                0
            ]
        },
        "DateTime": {},
        "PresenceData": {
            "presenceSureAvailable": [
                1
            ],
            "orderAvailable": [
                1
            ],
            "available": [
                1
            ],
            "title": [
                0
            ],
            "ending": [
                1
            ],
            "waiting": [
                1
            ],
            "id": [
                2
            ],
            "productId": [
                2
            ],
            "product": [
                14
            ],
            "__typename": [
                0
            ]
        },
        "ProductAttribute": {
            "id": [
                2
            ],
            "productId": [
                2
            ],
            "externalId": [
                2
            ],
            "attributeName": [
                0
            ],
            "attributeGroup": [
                0
            ],
            "attributeValues": [
                0
            ],
            "product": [
                14
            ],
            "__typename": [
                0
            ]
        },
        "ProductPaymentOption": {
            "id": [
                2
            ],
            "productId": [
                2
            ],
            "externalId": [
                2
            ],
            "optionName": [
                0
            ],
            "optionsComment": [
                0
            ],
            "product": [
                14
            ],
            "__typename": [
                0
            ]
        },
        "ProductDeliveryOption": {
            "id": [
                2
            ],
            "productId": [
                2
            ],
            "externalId": [
                2
            ],
            "optionName": [
                0
            ],
            "optionsComment": [
                0
            ],
            "product": [
                14
            ],
            "__typename": [
                0
            ]
        },
        "ProductData": {
            "id": [
                2
            ],
            "companyId": [
                2
            ],
            "externalId": [
                0
            ],
            "title": [
                0
            ],
            "url": [
                0
            ],
            "syncDate": [
                9
            ],
            "expirationDate": [
                9
            ],
            "productState": [
                15
            ],
            "description": [
                0
            ],
            "price": [
                0
            ],
            "keyWords": [
                0
            ],
            "jsonData": [
                0
            ],
            "jsonDataSchema": [
                0
            ],
            "company": [
                16
            ],
            "presence": [
                10
            ],
            "categories": [
                17
            ],
            "productPaymentOptions": [
                12
            ],
            "productDeliveryOptions": [
                13
            ],
            "productAttribute": [
                11
            ],
            "__typename": [
                0
            ]
        },
        "ProductState": {},
        "CompanyData": {
            "id": [
                2
            ],
            "sourceId": [
                2
            ],
            "externalId": [
                0
            ],
            "name": [
                0
            ],
            "url": [
                0
            ],
            "syncDate": [
                9
            ],
            "companyState": [
                18
            ],
            "jsonData": [
                0
            ],
            "jsonDataSchema": [
                0
            ],
            "products": [
                14
            ],
            "source": [
                19
            ],
            "__typename": [
                0
            ]
        },
        "CategoryData": {
            "id": [
                2
            ],
            "name": [
                0
            ],
            "url": [
                0
            ],
            "products": [
                14
            ],
            "supCategoryData": [
                17
            ],
            "__typename": [
                0
            ]
        },
        "CompanyState": {},
        "CompanySource": {
            "id": [
                2
            ],
            "name": [
                0
            ],
            "companies": [
                16
            ],
            "__typename": [
                0
            ]
        },
        "Query": {
            "categories": [
                6,
                {
                    "id": [
                        2
                    ],
                    "companyId": [
                        2
                    ]
                }
            ],
            "companies": [
                7
            ],
            "products": [
                3,
                {
                    "skip": [
                        2
                    ],
                    "take": [
                        2
                    ],
                    "companyId": [
                        2
                    ],
                    "categoryId": [
                        2
                    ]
                }
            ],
            "product": [
                8,
                {
                    "id": [
                        2,
                        "Int!"
                    ]
                }
            ],
            "__typename": [
                0
            ]
        },
        "Subscription": {
            "subscribeProductGetDate": [
                8
            ],
            "__typename": [
                0
            ]
        }
    }
}