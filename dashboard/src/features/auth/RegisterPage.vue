<script setup lang="ts">
import { request } from '@/axios'
import { useValidator } from '@/shared/hooks/useValidator'
import type { FormInstance, FormRules } from 'element-plus'
import { ref, type UnwrapRef } from 'vue'

const model = ref({
  username: '',
  password: '',
  fullName: '',
  phone: '',
})

const formRef = ref<FormInstance>()

const { required } = useValidator()

const rules: FormRules<UnwrapRef<typeof model>> = {
  username: [required()],
  password: [required()],
}

const handleSave = async () => {
  try {
    await formRef.value?.validate()
    console.log('submitted')
    const res = await request.post<string[]>('/user/register', {
      ...model.value,
      value: 'string',
    })
    console.log(res)
  } catch (error) {
    console.error(error)
  }
}
</script>

<template>
  <el-text class="text-center block text-xl!" type="primary" size="large">Register Page</el-text>
  <el-form
    ref="formRef"
    :rules="rules"
    :model="model"
    label-position="top"
    require-asterisk-position="right"
    @submit.prevent
  >
    <el-form-item prop="username" label="Username" required>
      <el-input v-model="model.username" />
    </el-form-item>
    <el-form-item prop="password" label="Password" required>
      <el-input v-model="model.password" />
    </el-form-item>
    <el-form-item prop="fullName" label="Full name">
      <el-input v-model="model.fullName" />
    </el-form-item>
    <el-form-item prop="phone" label="Phone">
      <el-input v-model="model.phone" />
    </el-form-item>

    <el-button @click="handleSave">Validate</el-button>
  </el-form>
</template>

<style scoped lang="scss"></style>
