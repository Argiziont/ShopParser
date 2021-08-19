import {
  FieldsSelection,
  GraphqlOperation,
  ClientOptions,
  Observable,
} from '@genql/runtime'
import { SubscriptionClient } from 'subscriptions-transport-ws'
import {
  QueryServiceRequest,
  QueryServicePromiseChain,
  QueryService,
  SubscriptionObjectTypeRequest,
  SubscriptionObjectTypeObservableChain,
  SubscriptionObjectType,
} from './schema'
export * from './schema'
export declare const createClient: (options?: ClientOptions) => Client
export declare const everything: { __scalar: boolean }
export declare const version: string

export interface Client {
  wsClient?: SubscriptionClient

  query<R extends QueryServiceRequest>(
    request: R & { __name?: string },
  ): Promise<FieldsSelection<QueryService, R>>

  subscription<R extends SubscriptionObjectTypeRequest>(
    request: R & { __name?: string },
  ): Observable<FieldsSelection<SubscriptionObjectType, R>>

  chain: {
    query: QueryServicePromiseChain

    subscription: SubscriptionObjectTypeObservableChain
  }
}

export type QueryResult<fields extends QueryServiceRequest> = FieldsSelection<
  QueryService,
  fields
>

export declare const generateQueryOp: (
  fields: QueryServiceRequest & { __name?: string },
) => GraphqlOperation
export type SubscriptionResult<
  fields extends SubscriptionObjectTypeRequest
> = FieldsSelection<SubscriptionObjectType, fields>

export declare const generateSubscriptionOp: (
  fields: SubscriptionObjectTypeRequest & { __name?: string },
) => GraphqlOperation
