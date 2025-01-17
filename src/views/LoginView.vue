<template>
    <div class="login-view">
      <LoginForm />
    </div>
  </template>
  
  <script>
  import LoginForm from '../components/LoginForm.vue';
  
  export default {
    components: {
      LoginForm
    }
  };
  </script>
  
  <style lang="scss">
  .login-view {
    height: 100vh;
    display: flex;
    justify-content: center;
    align-items: center;
    background-image: url('../assets/img/imagensesion.jpg');
    background-size: cover;
    background-position: center;
  }
  </style>
  