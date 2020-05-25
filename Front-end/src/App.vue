<template>
  <div id="app">
    <h1>Muzika ir barai</h1>
      <ul>
        <router-link to="/EventList">Renginiai</router-link>
        <router-link to="/Map">Žemėlapis</router-link>
        <router-link to="/Playlist">Grojaraštis</router-link>
        <router-link to="/Clients">Lankytojai</router-link>
        <router-link to="/Bills">Sąskaitos</router-link>
        <router-link to="/PlaylistForUsers">Grojaraštis Lankytojams</router-link>
        <router-link to="/Top10Songs">Top 10 dainų</router-link>        
    </ul>
    <router-view/>
  </div>
</template>

<script>
export default {
  name: 'App'
}
</script>

<style>
@import url("https://fonts.googleapis.com/css?family=Material+Icons");
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
