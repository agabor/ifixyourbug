<template>
	<editor class="border-radius-lg" v-model="text" :placeholder="placeholder" :init="{
		height: 300,
		menubar: false,
		plugins: [
			'advlist', 'autolink',
			'lists', 'link', 'image', 'charmap', 'preview', 'anchor', 'searchreplace', 'visualblocks',
			'fullscreen', 'insertdatetime', 'media', 'table', 'help'
		],
		toolbar:
			'undo redo | casechange blocks | bold italic backcolor | \
					alignleft aligncenter alignright alignjustify | \
					bullist numlst checklist outdent indent | removeformat | a11ycheck code table help'
	}" />
</template>

<script>
import { ref, watch } from 'vue';
import Editor from '@tinymce/tinymce-vue';

export default {
	name: 'TextEditor',
	components: { Editor },
	props: {
		modelValue: String,
		placeholder: String
	},
	emits: ['update:modelValue'],
	setup(props, context) {
		const text = ref(props.modelValue);

		watch(text, () => {
			context.emit('update:modelValue', text);
		})

		return { text }
	}
}
</script>

<style>
.tox-statusbar {
	display: none !important;
}
</style>