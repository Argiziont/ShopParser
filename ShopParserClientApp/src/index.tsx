import React from 'react';
import ReactDOM from 'react-dom';
import { AppPage } from './_pages/AppPage';
import reportWebVitals from './reportWebVitals';
import {
  ApolloClient,
  InMemoryCache,
  ApolloProvider,
  split,
  HttpLink,
} from "@apollo/client";
import { WebSocketLink } from '@apollo/client/link/ws';
import { getMainDefinition } from '@apollo/client/utilities';


const wsLink = new WebSocketLink({
  uri: 'wss://localhost:5001/graphql',
  options: {
    reconnect: true
  }
});
const httpLink = new HttpLink({
  uri: 'https://localhost:5001/graphql/'
});
const splitLink = split(
  ({ query }) => {
    const definition = getMainDefinition(query);
    return (
      definition.kind === 'OperationDefinition' &&
      definition.operation === 'subscription'
    );
  },
  wsLink,
  httpLink,
);
const client = new ApolloClient({
  link: splitLink,
  cache: new InMemoryCache()
});
ReactDOM.render(
  <ApolloProvider client={client}>
    <AppPage />
    </ApolloProvider>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
