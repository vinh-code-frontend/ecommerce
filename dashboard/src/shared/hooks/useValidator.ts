import type { FormItemRule } from 'element-plus'

export const useValidator = () => {
  const required = (message?: string): FormItemRule => {
    return {
      validator: (_, value, cb) => {
        if (!value?.trim()) {
          cb(message || 'This is required field')
        } else {
          cb()
        }
      },
    }
  }

  return {
    required,
  }
}
