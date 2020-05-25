import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '@/components/HelloWorld'
import EventList from '@/components/EventList'
import Event from '@/components/Event'
import MapPage from '@/components/Map.vue'
import BarDetails from '@/components/BarDetails.vue'
import Reviews from '@/components/Reviews.vue'
import Playlist from '@/components/Playlist.vue'
import Clients from '@/components/Clients.vue'
import Bills from '@/components/Bills.vue'
import AddSong from '@/components/AddSong.vue'
import PlaylistForUsers from '@/components/PlaylistForUsers.vue'
import Top10Songs from '@/components/Top10Songs.vue'


Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'HelloWorld',
      component: HelloWorld
    },
    {
      path: '/EventList',
      name: 'EventList',
      component: EventList
    },
    {
      path: '/Event',
      name: 'Event',
      component: Event
    },
    {
      path: '/Map',
      name: 'Map',
      component: MapPage
    },
    {
      path: '/BarDetails',
      name: 'BarDetails',
      component: BarDetails
    },
    {
      path: '/Reviews',
      name: 'Reviews',
      component: Reviews
    },
    {
      path: '/Playlist',
      name: 'Playlist',
      component: Playlist
    },
    {
      path: '/Clients',
      name: 'Clients',
      component: Clients
    },
    {
      path: '/Bills',
      name: 'Bills',
      component: Bills
    },
    {
      path: '/AddSong',
      name: 'AddSong',
      component: AddSong
    },
    {
      path: '/PlaylistForUsers',
      name: 'PlaylistForUsers',
      component: PlaylistForUsers
    },    
    {
      path: '/Top10Songs',
      name: 'Top10Songs',
      component: Top10Songs
    }    
  ]
})
