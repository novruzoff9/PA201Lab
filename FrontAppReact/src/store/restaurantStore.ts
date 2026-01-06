import { create } from 'zustand';
import { persist } from 'zustand/middleware';

export interface Category {
  id: string;
  name: string;
  description: string;
  createdAt: string;
}

export interface Product {
  id: string;
  name: string;
  description: string;
  price: number;
  categoryId: string;
  image?: string;
  createdAt: string;
}

interface RestaurantStore {
  categories: Category[];
  products: Product[];
  
  // Category actions
  addCategory: (category: Omit<Category, 'id' | 'createdAt'>) => void;
  updateCategory: (id: string, category: Partial<Category>) => void;
  deleteCategory: (id: string) => void;
  
  // Product actions
  addProduct: (product: Omit<Product, 'id' | 'createdAt'>) => void;
  updateProduct: (id: string, product: Partial<Product>) => void;
  deleteProduct: (id: string) => void;
}

export const useRestaurantStore = create<RestaurantStore>()(
  persist(
    (set) => ({
      categories: [
        { id: '1', name: 'Salatlar', description: 'Təzə və sağlam salatlar', createdAt: new Date().toISOString() },
        { id: '2', name: 'Əsas yeməklər', description: 'Ləzzətli əsas yeməklər', createdAt: new Date().toISOString() },
        { id: '3', name: 'İçkilər', description: 'Soyuq və isti içkilər', createdAt: new Date().toISOString() },
      ],
      products: [
        { id: '1', name: 'Sezar salatı', description: 'Klassik sezar salatı', price: 12.99, categoryId: '1', createdAt: new Date().toISOString() },
        { id: '2', name: 'Toyuq kabab', description: 'Həndirə marinad edilmiş toyuq', price: 18.99, categoryId: '2', createdAt: new Date().toISOString() },
        { id: '3', name: 'Limonad', description: 'Təzə limonadan hazırlanmış', price: 4.99, categoryId: '3', createdAt: new Date().toISOString() },
      ],

      addCategory: (category) =>
        set((state) => ({
          categories: [
            ...state.categories,
            {
              ...category,
              id: crypto.randomUUID(),
              createdAt: new Date().toISOString(),
            },
          ],
        })),

      updateCategory: (id, category) =>
        set((state) => ({
          categories: state.categories.map((c) =>
            c.id === id ? { ...c, ...category } : c
          ),
        })),

      deleteCategory: (id) =>
        set((state) => ({
          categories: state.categories.filter((c) => c.id !== id),
          products: state.products.filter((p) => p.categoryId !== id),
        })),

      addProduct: (product) =>
        set((state) => ({
          products: [
            ...state.products,
            {
              ...product,
              id: crypto.randomUUID(),
              createdAt: new Date().toISOString(),
            },
          ],
        })),

      updateProduct: (id, product) =>
        set((state) => ({
          products: state.products.map((p) =>
            p.id === id ? { ...p, ...product } : p
          ),
        })),

      deleteProduct: (id) =>
        set((state) => ({
          products: state.products.filter((p) => p.id !== id),
        })),
    }),
    {
      name: 'restaurant-storage',
    }
  )
);
