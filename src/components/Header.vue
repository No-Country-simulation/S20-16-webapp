<template>
  <div class="row mb-4 header">
    <div class="col-md-8">
      <h4>{{ carrera || 'Carrera no disponible' }}</h4>
    </div>
    <div class="col-md-4 d-flex justify-content-end">
      <div class="student-info">
        <img
          src="../assets/img/estudiante2.jpg"
          alt="Foto Estudiante"
          class="student-photo"
        />
        <h4>{{ nombre || 'Estudiante' }}</h4>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Header',

  props: {
    carrera: {
      type: String,
      default: 'Carrera no disponible',
    },
    nombre: {
      type: String,
      default: 'Estudiante',
    },
  },
};
</script>

<style scoped>
.header h4 {
  margin-bottom: 20px;
  font-size: 20px;
}


.student-info {
  display: flex;
  align-items: center;
}

.student-photo {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  margin-right: 10px;
  align-items: center;
}
</style>
