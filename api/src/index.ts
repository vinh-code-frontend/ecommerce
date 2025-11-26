import express, { Express, Request, Response } from 'express'
import dotenv from 'dotenv'

// Import sử dụng Alias (được định nghĩa trong tsconfig.json)
import { getHello } from '@/controllers/hello.controller'

dotenv.config()
const x: undefined = undefined

const app: Express = express()
const port = process.env.PORT || 3000

app.use(express.json())

app.get('/', (req: Request, res: Response) => {
  res.send('Server is running!')
})

// Sử dụng controller đã import qua alias
app.get('/api/hello', getHello)

app.listen(port, () => {
  console.log(`⚡️[server]: Server đang chạy tại http://localhost:${port}`)
  console.log(`⚡️[info]: Thử sửa file để kiểm tra Hot Reload...`)
})
