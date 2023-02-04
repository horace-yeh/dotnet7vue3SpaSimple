<template>
    <v-data-table
      :headers="headers"
      :items="desserts"
      :sort-by="[{ key: 'calories', order: 'asc' }]"
      class="elevation-1"
    >
      <template v-slot:top>
        <v-toolbar
          flat
        >
          <v-toolbar-title>My CRUD</v-toolbar-title>
          <v-divider
            class="mx-4"
            inset
            vertical
          ></v-divider>
          <v-spacer></v-spacer>
          <v-dialog
            v-model="dialog"
            max-width="500px"
          >
            <template v-slot:activator="{ props }">
              <v-btn
                color="primary"
                dark
                class="mb-2"
                v-bind="props"
              >
                New Item
              </v-btn>
            </template>
            <v-card>
              <v-card-title>
                <span class="text-h5">{{ formTitle }}</span>
              </v-card-title>
  
              <v-card-text>
                <v-container>
                  <v-row>
                    <v-col
                      cols="12"
                      sm="6"
                      md="4"
                    >
                      <v-text-field
                        v-model="editedItem.Name"
                        label="Dessert name"
                      ></v-text-field>
                    </v-col>
                    <v-col
                      cols="12"
                      sm="6"
                      md="4"
                    >
                      <v-text-field
                        v-model="editedItem.Calories"
                        label="Calories"
                      ></v-text-field>
                    </v-col>
                    <v-col
                      cols="12"
                      sm="6"
                      md="4"
                    >
                      <v-text-field
                        v-model="editedItem.Fat"
                        label="Fat (g)"
                      ></v-text-field>
                    </v-col>
                    <v-col
                      cols="12"
                      sm="6"
                      md="4"
                    >
                      <v-text-field
                        v-model="editedItem.Carbs"
                        label="Carbs (g)"
                      ></v-text-field>
                    </v-col>
                    <v-col
                      cols="12"
                      sm="6"
                      md="4"
                    >
                      <v-text-field
                        v-model="editedItem.Protein"
                        label="Protein (g)"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                </v-container>
              </v-card-text>
  
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn
                  color="blue-darken-1"
                  variant="text"
                  @click="close"
                >
                  Cancel
                </v-btn>
                <v-btn
                  color="blue-darken-1"
                  variant="text"
                  @click="save"
                >
                  Save
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
          <v-dialog v-model="dialogDelete" max-width="500px">
            <v-card>
              <v-card-title class="text-h5">Are you sure you want to delete this item?</v-card-title>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue-darken-1" variant="text" @click="closeDelete">Cancel</v-btn>
                <v-btn color="blue-darken-1" variant="text" @click="deleteItemConfirm">OK</v-btn>
                <v-spacer></v-spacer>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-toolbar>
      </template>
      <template v-slot:item.actions="{ item }">
        <v-icon
          size="small"
          class="me-2"
          @click="editItem(item.raw)"
        >
          mdi-pencil
        </v-icon>
        <v-icon
          size="small"
          @click="deleteItem(item.raw)"
        >
          mdi-delete
        </v-icon>
      </template>
      <template v-slot:no-data>
        <v-btn
          color="primary"
          @click="initialize"
        >
          Reset
        </v-btn>
      </template>
    </v-data-table>
  </template>

<script>
import { ref, reactive, computed,onMounted,watch,nextTick ,toRefs } from "vue";
import { apiGetProductList,apiAddProduct,apiEditProduct,apiDeleteProduct} from "../services/api.service.js";
export default {
    setup(){


        let dialog = ref(false);
        let dialogDelete = ref(false);
        const headers = reactive([
        {
          title: 'Dessert (100g serving)',
          align: 'start',
          sortable: false,
          key: 'Name',
        },
        { title: 'Calories', key: 'Calories' },
        { title: 'Fat (g)', key: 'Fat' },
        { title: 'Carbs (g)', key: 'Carbs' },
        { title: 'Protein (g)', key: 'Protein' },
        { title: 'Actions', key: 'actions', sortable: false },
        ]);

        let editedIndex = ref(-1);
        let editedItem = reactive({
        Name: '',
        Calories: 0,
        Fat: 0,
        Carbs: 0,
        Protein: 0,
        });
        const defaultItem = reactive({
        Name: '',
        Calories: 0,
        Fat: 0,
        Carbs: 0,
        Protein: 0,
        });

        const dessert = reactive({desserts:[]});

        const initialize = () => {
          apiGetProductList().then(res=>{
            if(res.status == 200)
            dessert.desserts = res.data;
          })
        };

        const formTitle  = computed(()=> editedIndex.value === -1 ? 'New Item' : 'Edit Item');

        onMounted(()=> {
            initialize();
        });

        watch(dialog, (val,oldVa)=>{
            val || close()
        });

        watch(dialogDelete, (val,oldVa)=>{
            val || closeDelete()
        });

        const editItem =(item)=>{
            editedIndex.value = dessert.desserts.indexOf(item);
            Object.assign(editedItem, item);
            dialog.value = true;
        }; 

        const deleteItem = (item)=>{
            editedIndex.value = dessert.desserts.indexOf(item);
            Object.assign(editedItem, item);
            dialogDelete.value = true;
        };

        const deleteItemConfirm = ()=>{
          const idx = editedIndex.value;
          const item = dessert.desserts[idx];
          apiDeleteProduct(item).then(res=>{
            if(res.status == 200){
              initialize();
              closeDelete();
            }
          })
        };

        const close = ()=>{
            dialog.value = false;
            nextTick(() => {
                Object.assign(editedItem, defaultItem);
                editedIndex.value = -1;
            });
        }

        const closeDelete = ()=>{
            dialogDelete.value = false
            nextTick(() => {
                Object.assign(editedItem, defaultItem);
                editedIndex.value = -1;
            })
        }

        const save = () => {
            const temp = Object.assign({}, editedItem);
            const idx = editedIndex.value;          
            if (idx > -1) {
              apiEditProduct(temp).then(res=>{
                if(res.status == 200){
                  dessert.desserts[idx] = temp;
                }
              })            
            } 
            else {
              apiAddProduct(temp).then(res=>{
                if(res.status == 200){
                  initialize();
                }
              })                
            }
            close();
        }

        return { headers,...toRefs(dessert),editedIndex,editedItem,
                 defaultItem,dialog,dialogDelete,formTitle,editItem, 
                 deleteItem,deleteItemConfirm,close,closeDelete,
                 save,initialize
               }
    }
}
</script>