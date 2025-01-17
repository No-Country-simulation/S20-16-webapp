<template>
    <div class="pass-view">
      <PassForm />
    </div>
  </template>
  
  <script>
  import PassForm from '../components/PassForm.vue';
  
  export default {
    components: {
      PassForm
    }
  };
  </script>
  
  <style lang="scss">
  .pass-view {
    height: 100vh;
    display: flex;
    justify-content: center;
    align-items: center;
    background-image: url('../assets/img/imagensesion.jpg');
    background-size: cover;
    background-position: center;
  }
  </style>
  