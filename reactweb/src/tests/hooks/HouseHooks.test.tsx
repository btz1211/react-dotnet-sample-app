import { renderHook, waitFor } from '@testing-library/react';
import { House } from '../../models/House';
import { useFetchHouse, useFetchHouses } from '../../models/HouseHooks';
import { createWrapper } from '../utils/query-client-wrapper';
import { server } from '../utils/test-server';


const testServer = server;

describe('House Hooks', () => {
    describe('Fetch Houses', () => {
        test('success fetch', async () => {
            const { result } = renderHook(() => useFetchHouses(), { wrapper: createWrapper() });

            await waitFor(() => expect(result.current.isSuccess).toBeTruthy());
            expect(result.current.data?.length).toBe(2);
            
            const houses: House[] = result.current.data!;
            expect(houses[0].id).toBe("house-1");
            expect(houses[1].id).toBe("house-2");
        })
    });

    describe('Fetch House', () => {
        test('success fetch', async () => {
            const { result } = renderHook(() => useFetchHouse("house-1"), { wrapper: createWrapper() });

            await waitFor(() => expect(result.current.isSuccess).toBeTruthy());
            
            const house: House = result.current.data!;
            expect(house.id).toBe("house-1");
        })
    })
})