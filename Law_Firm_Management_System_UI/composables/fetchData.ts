//
// Composable for $fetch
//
const baseURL = useRuntimeConfig().public.baseURL

async function fetchResult(url: string, method: any, body: any = null) {
  return $fetch(baseURL + url, {
    method: method,
    headers: { 'Authorization': `${useAuth().token.value}` },
    body: body ? JSON.stringify(body) : undefined,
  })
}

export const fetchData = {
  $post(requestURL: string, body: {}) {
    return fetchResult(requestURL, 'POST', body)
  },
  $put(requestURL: string, body: {}) {
    return fetchResult(requestURL, 'PUT', body)
  },
  $get(requestURL: string, params: {}) {
    return useFetch(baseURL + requestURL, {
      headers: { 'Authorization': `${useAuth().token.value}` },
      params: params,
    })
  },
  $delete(requestURL: string) {
    return fetchResult(requestURL, 'DELETE')
  }
}