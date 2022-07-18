<template>
  <input class="form-control" :placeholder="$t('searchBar.search')" type="text" v-model="searchText" @input="search">
</template>

<script>
import { ref } from 'vue';
import { useI18n } from "vue-i18n";

export default {
  name: 'SearchBar',
  props: {
    modelValue: Array,
    data: Array
  },
  emits: ['update:modelValue'],
  setup(props, context) {
    const searchText = ref('');
    const { tm } = useI18n();

    async function search() {
      let filteredData = [];
      if(searchText.value !== '') {
        for (let i = 0; i < props.data.length; i++) {
          let addElement = false;
          for(const property in props.data[i]) {
            let element = props.data[i][property] ? props.data[i][property].toString() : '';
            if(property == 'specificPlatformVersion' || property == 'bugDescription' || property == 'messages' || property == 'gitAccessId' || property == 'paymentToken' || property == 'clientId' || property == 'id'){
              continue;
            }
            if(property == 'framework') {
              if(element == '1')
                element = tm('framework.option2');
              else 
                element = tm('framework.option1');
            }
            if(element.toUpperCase().includes(searchText.value.toUpperCase())) {
              addElement = true;
              break;
            }
          }
          if(addElement) {
            filteredData.push(props.data[i]);
          }
        }
        context.emit('update:modelValue', filteredData);
      } else {
        context.emit('update:modelValue', props.data);
      }
    }

    return { searchText, search }
  }
}
</script>