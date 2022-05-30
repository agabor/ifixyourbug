<template>
	<editor class="border-radius-lg" v-model="text" @change="$emit('update:modelValue', text)" :placeholder="placeholder"
		:api-key="key"
		:init="{
			height: 300,
			menubar: false,
			plugins: [
				'a11ychecker','advlist','advcode','advtable','autolink','checklist','export',
				'lists','link','image','charmap','preview','anchor','searchreplace','visualblocks',
				'powerpaste','fullscreen','formatpainter','insertdatetime','media','table','help','wordcount'
			],
			toolbar:
				'undo redo | casechange blocks | bold italic backcolor | \
				alignleft aligncenter alignright alignjustify | \
				bullist numlst checklist outdent indent | removeformat | a11ycheck code table help'
		}"
	/>
</template>

<script>
import { ref } from 'vue';
import Editor from '@tinymce/tinymce-vue';

export default {
  name: 'TextEditor',
  components: { Editor },
  props: {
    modelValue: String,
		placeholder: String
  },
  emits:['update:modelValue'],
  setup(props) {
		const text = ref(props.modelValue);
		const key = process.env.VUE_APP_TINY_API_KEY;
		return { text, key }
	}
}
</script>

<style>
	.tox-tinymce {
		border-radius: 0.625rem!important;
	}
	.tox-statusbar{
			display: none!important;
	}
</style>