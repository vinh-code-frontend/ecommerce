import axios from 'axios'

export const request = axios.create({
  baseURL: 'http://localhost:5052/api',
})

request.interceptors.request.use((config) => {
  return config
})

request.interceptors.response.use((response) => {
  console.log(response.data)
  return response
})
