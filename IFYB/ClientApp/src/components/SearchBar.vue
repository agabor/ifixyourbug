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
    data: Array,
    properties: Array
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
            if(!props.properties.includes(property)){
              continue;
            }
            if(property == 'framework') {
              if(element == '1')
                element = tm('framework.option2');
              else 
                element = tm('framework.option1');
            }
            if(property == 'state') {
              switch (element) {
                case '1':
                  element = tm('orderList.accepted');
                  break;
                case '2':
                  element = tm('orderList.rejected');
                  break;
                case '3':
                  element = tm('orderList.payed');
                  break;
                case '4':
                  element = tm('orderList.completed');
                  break;
                case '6':
                  element = tm('orderList.refundable');
                  break;
                default:
                  element = tm('orderList.submitted');
              }
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