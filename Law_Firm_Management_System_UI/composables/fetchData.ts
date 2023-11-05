//
// Composable for $fetch
//

const baseURL = useRuntimeConfig().public.baseURL

async function fetchResult(url: string, method: any, body: any = null) {
  return $fetch(baseURL + url, {
    method: method,
    headers: { 'Content-Type': 'application/json' },
    body: body ? JSON.stringify(body) : undefined,
  })
}

export const fetchData = {
  $post(requestURL: string, body: {}) {
    return fetchResult(requestURL, 'POST', body)
  },
  $put(requestURL: string, body: {}){
    return fetchResult(requestURL, 'PUT', body)
  },
  $get(requestURL: string){
    return fetchResult(requestURL, 'GET')
  },
  $delete(requestURL: string){
    return fetchResult(requestURL, 'DELETE')
  }
}